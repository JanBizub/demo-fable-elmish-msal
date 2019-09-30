// ts2fable 0.6.2
module rec UserAgentApplication
open System
open Fable.Core
open Fable.Core.JS
open Browser.Types

type Authority                      = Authority.Authority
type Logger                         = Logger.Logger
type Storage                        = Storage.Storage
type Account                        = Account.Account
type Configuration                  = Configuration.Configuration
type AuthenticationParameters       = AuthenticationParameters.AuthenticationParameters
type AuthError                      = AuthError.AuthError
type AuthResponse                   = AuthResponse.AuthResponse

type [<AllowNullLiteral>] IExports =
    abstract UserAgentApplication: UserAgentApplicationStatic

type [<AllowNullLiteral>] Window =
    abstract msal: Object with get, set
    abstract CustomEvent: CustomEvent with get, set
    abstract Event: Event with get, set
    abstract activeRenewals: TypeLiteral_01 with get, set
    abstract renewStates: array<string> with get, set
    abstract callbackMappedToRenewStates: TypeLiteral_01 with get, set
    abstract promiseMappedToRenewStates: TypeLiteral_01 with get, set
    abstract openedWindows: array<Window> with get, set
    abstract requestType: string with get, set

type [<AllowNullLiteral>] CacheResult =
    abstract errorDesc: string with get, set
    abstract token: string with get, set
    abstract error: string with get, set

type [<AllowNullLiteral>] ResponseStateInfo =
    abstract state: string with get, set
    abstract stateMatch: bool with get, set
    abstract requestType: string with get, set

type [<AllowNullLiteral>] authResponseCallback =
    [<Emit "$0($1...)">] abstract Invoke: authErr: AuthError * ?response: AuthResponse -> unit

type [<AllowNullLiteral>] tokenReceivedCallback =
    [<Emit "$0($1...)">] abstract Invoke: response: AuthResponse -> unit

type [<AllowNullLiteral>] errorReceivedCallback =
    [<Emit "$0($1...)">] abstract Invoke: authErr: AuthError * accountState: string -> unit

/// UserAgentApplication class
/// 
/// Object Instance that the developer can use to make loginXX OR acquireTokenXX functions
type [<AllowNullLiteral>] UserAgentApplication =
    abstract cacheStorage: Storage with get, set
    abstract authorityInstance: Authority with get, set
    /// setter for the authority URL
    /// Method to manage the authority URL.
    abstract authority: string with get, set
    /// Get the current authority instance from the MSAL configuration object
    abstract getAuthorityInstance: unit -> Authority
    abstract handleRedirectCallback: tokenReceivedCallback: tokenReceivedCallback * errorReceivedCallback: errorReceivedCallback -> unit
    abstract handleRedirectCallback: authCallback: authResponseCallback -> unit
    /// Use when initiating the login process by redirecting the user's browser to the authorization endpoint.
    abstract loginRedirect: ?request: AuthenticationParameters -> unit
    /// Use when you want to obtain an access_token for your API by redirecting the user's browser window to the authorization endpoint.
    abstract acquireTokenRedirect: request: AuthenticationParameters -> unit
    /// <param name="hash">- Hash passed from redirect page.</param>
    abstract isCallback: hash: string -> bool
    /// Use when initiating the login process via opening a popup window in the user's browser
    abstract loginPopup: ?request: AuthenticationParameters -> Promise<AuthResponse>
    /// Use when you want to obtain an access_token for your API via opening a popup window in the user's browser
    abstract acquireTokenPopup: request: AuthenticationParameters -> Promise<AuthResponse>
    /// Use this function to obtain a token before every call to the API / resource provider
    /// 
    /// MSAL return's a cached token when available
    /// Or it send's a request to the STS to obtain a new token using a hidden iframe.
    abstract acquireTokenSilent: request: AuthenticationParameters -> Promise<AuthResponse>
    abstract isInIframe: unit -> bool
    /// Use to log out the current user, and redirect the user to the postLogoutRedirectUri.
    /// Default behaviour is to redirect the user to `window.location.href`.
    abstract logout: unit -> unit
    abstract clearCache: unit -> unit
    /// <param name="accessToken"></param>
    abstract clearCacheForScope: accessToken: string -> unit
    /// <param name="hash">-  Hash passed from redirect page</param>
    abstract getResponseState: hash: string -> ResponseStateInfo
    abstract saveTokenFromHash: hash: string * stateInfo: ResponseStateInfo -> AuthResponse
    /// Returns the signed in account
    /// (the account object is created at the time of successful login)
    /// or null when no state is found
    abstract getAccount: unit -> Account
    abstract getAccountState: state: string -> string
    /// Use to get a list of unique accounts in MSAL cache based on homeAccountIdentifier.
    abstract getAllAccounts: unit -> array<Account>
    /// <param name="scopes"></param>
    /// <param name="state"></param>
    abstract getCachedTokenInternal: scopes: array<string> * account: Account * state: string -> AuthResponse
    /// <param name="endpoint"></param>
    abstract getScopesForEndpoint: endpoint: string -> array<string>
    /// Return boolean flag to developer to help inform if login is in progress
    abstract getLoginInProgress: unit -> bool
    /// <param name="loginInProgress"></param>
    abstract setloginInProgress: loginInProgress: bool -> unit
    abstract getAcquireTokenInProgress: unit -> bool
    /// <param name="acquireTokenInProgress"></param>
    abstract setAcquireTokenInProgress: acquireTokenInProgress: bool -> unit
    abstract getLogger: unit -> Logger
    /// Use to get the redirect uri configured in MSAL or null.
    /// Evaluates redirectUri if its a function, otherwise simply returns its value.
    abstract getRedirectUri: unit -> string
    /// Use to get the post logout redirect uri configured in MSAL or null.
    /// Evaluates postLogoutredirectUri if its a function, otherwise simply returns its value.
    abstract getPostLogoutRedirectUri: unit -> string
    /// Use to get the current {@link Configuration} object in MSAL
    abstract getCurrentConfiguration: unit -> Configuration

/// UserAgentApplication class
/// 
/// Object Instance that the developer can use to make loginXX OR acquireTokenXX functions
type [<AllowNullLiteral>] UserAgentApplicationStatic =
    [<Emit "new $0($1...)">] abstract Create: configuration: Configuration -> UserAgentApplication

type [<AllowNullLiteral>] TypeLiteral_01 =
    interface end