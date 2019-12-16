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
let aad_clientID  = "<CLIENT ID>"
let aad_authority = "https://login.microsoftonline.com/<TENANT ID>"
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

let userAgentApplication = 
  msalConfiguration  
  |> Fable.Msal.initUserAgentApplication

// => App Types ========================================================================================================
type AppModel = 
  { isUiLoading : bool
    nameFromAPI : string }
  static member Empty =
    { isUiLoading = false
      nameFromAPI = "" }

type Msg =
  | Login
  | RequestNameFromAPI
  | ReceiveRequestFromAPI       of string
  | RestError                   of exn

// => Calling Rest API =================================================================================================
module REST =
  let apiUrl = "https://localhost:44338/car"
  
  let login (ua: UserAgentApplication) =
    ua.loginRedirect()
  
  let loadCarNames (ua: UserAgentApplication) =
    let request () = promise {
      let! ats = ua.acquireTokenSilent(authParameters)
      let! r = fetch apiUrl [requestHeaders [(Authorization (sprintf "Bearer %s" ats.idToken.rawIdToken))]] 
      let! t = r.text()
      return Decode.Auto.unsafeFromString<string> t
    }
    Cmd.OfPromise.either request () ReceiveRequestFromAPI RestError

// => App State ========================================================================================================
let update (ua: UserAgentApplication) msg model =
  match msg with
  | Login ->  
    ua.loginRedirect(authParameters) 
    model, []

  | RequestNameFromAPI ->
    model, REST.loadCarNames ua

  | ReceiveRequestFromAPI nameFromApi ->
    { model with nameFromAPI = nameFromApi }, []

  | RestError exc -> 
    match exc.Message with
    | "User login is required." -> 
      REST.login ua
      model, []
    | _  -> model, []
  
let init a = 
 AppModel.Empty, []

// => View to render ===================================================================================================
let appView model dispatch =
  div [] [
    hr []
    str (sprintf "Car Name: %s" model.nameFromAPI)
    hr []
    button [OnClick (fun _ -> RequestNameFromAPI |> dispatch )] ["Load Car Name From API" |> str]
    hr [] ]

Program.mkProgram init (update userAgentApplication) appView
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "aad-demo-app"
//#if DEBUG
|> Program.withDebugger
//#endif
|> Program.run