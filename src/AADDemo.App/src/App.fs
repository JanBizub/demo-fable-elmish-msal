module App
open Elmish
open Elmish.Navigation
open Elmish.React
open Elmish.Debug
open AuthenticationParameters
open UserAgentApplication
open Configuration
open Fable.React
open Fable.React.Props
open Thoth.Json
open Fetch

// => Authentication - MSAL ============================================================================================
let aad_clientID  = "<guid client id>"
let aad_authority = "https://login.microsoftonline.com/<guid tenant id>"
let aad_replyUrl  = "https://localhost:1010/"

let authOptions = {
  clientId                  = aad_clientID
  authority                 = Some aad_authority
  validateAuthority         = None
  redirectUri               = Some aad_replyUrl
  postLogoutRedirectUri     = None
  navigateToLoginRequestUrl = true
  }
let msalConfiguration = {
  auth      = authOptions
  cache     = None
  system    = None
  framework = None
  }
let authParameters = {
  scopes               = Some [|aad_clientID|]
  extraScopesToConsent = None
  prompt               = None
  extraQueryParameters = None
  claimsRequest        = None
  authority            = Some aad_authority
  state                = None
  correlationId        = None
  account              = None
  sid                  = None
  loginHint            = None
  }

let userAgentApplication = msalConfiguration  |> Fable.Msal.initUserAgentApplication

// => App Types ========================================================================================================
type AppModel = 
  { isUiLoading : bool
    nameFromAPI : string }
  static member Empty =
    { isUiLoading = false
      nameFromAPI = "" }

type BearerToken = string

type Msg =
  | Login
  | AcquireTokenSilent          of ( BearerToken -> Msg )
  | AcquireTokenSilentError     of exn
  | GetNameFromAPI
  | RequestNameFromAPI          of BearerToken
  | ReceiveRequestFromAPI       of string
  | RestError                   of exn

// => Calling Rest API =================================================================================================
let apiUrl = "https://localhost:44338/car"

let private fetchDropdownValues token =
  promise {
    let! r = fetch apiUrl [requestHeaders [(Authorization (sprintf "Bearer %s" token))]] 
    let! t = r.text()
    return Decode.Auto.unsafeFromString<string> t
  }
let loadDropdownValues token =
  Cmd.OfPromise.either
    fetchDropdownValues token
    ReceiveRequestFromAPI
    RestError

// => App State ========================================================================================================
let update ( ua: UserAgentApplication ) msg model =
  match msg with
  | Login ->  
    ua.loginRedirect(authParameters) 
    model, []

  // Use this function to obtain a token before every call to the API / resource provider
  // https://htmlpreview.github.io/?https://raw.githubusercontent.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-core/docs/classes/_useragentapplication_.useragentapplication.html#acquiretokensilent
  | AcquireTokenSilent tokenToMsg ->
    let acquireTokenS () =
      promise {
        let! res = ua.acquireTokenSilent(authParameters)
        return res }

    model, Cmd.OfPromise.either
      acquireTokenS ()
      (fun authResponse -> authResponse.idToken.rawIdToken |> tokenToMsg)
      AcquireTokenSilentError

  //todo: handle errors correctly. This might end in endless loop. :]
  | AcquireTokenSilentError e ->    
    model, Login |> Cmd.ofMsg

  // this calls AcquireTokenSilent and if it succeeded, it passes token to RequestNameFromAPI and calls it
  | GetNameFromAPI -> 
    model, AcquireTokenSilent(fun tk -> tk |> RequestNameFromAPI) |> Cmd.ofMsg

  | RequestNameFromAPI token ->
    model, loadDropdownValues token

  | ReceiveRequestFromAPI nameFromApi ->
    { model with nameFromAPI = nameFromApi }, []

  | RestError error ->
    model, []
  
let init a = 
 AppModel.Empty, []

// => View to render ===================================================================================================
let appView model dispatch =
  div [] [
    "car name:"       |> str
    model.nameFromAPI |> str
    button [OnClick (fun _ -> GetNameFromAPI |> dispatch )] ["Load Car Name From API" |> str] ]

Program.mkProgram init (update userAgentApplication) appView
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
//#if DEBUG
|> Program.withDebugger
//#endif
|> Program.run