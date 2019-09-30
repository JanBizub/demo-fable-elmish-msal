// ts2fable 0.6.2
module rec Configuration
open Fable.Core

type Logger = Logger.Logger

type [<StringEnum>] [<RequireQualifiedAccess>] CacheLocation =
    | LocalStorage
    | SessionStorage

type AuthOptions = {
    clientId                  : string
    authority                 : string option
    validateAuthority         : bool option
    redirectUri               : string option
    postLogoutRedirectUri     : string option
    navigateToLoginRequestUrl : bool
    }

type [<AllowNullLiteral>] CacheOptions =
    abstract cacheLocation : CacheLocation option with get, set
    abstract storeAuthStateInCookie : bool option with get, set

type [<AllowNullLiteral>] SystemOptions =
    abstract logger: Logger option with get, set
    abstract loadFrameTimeout: float option with get, set
    abstract tokenRenewalOffsetSeconds: float option with get, set
    abstract navigateFrameWait: float option with get, set

type [<AllowNullLiteral>] FrameworkOptions =
    abstract isAngular: bool option with get, set
    abstract unprotectedResources: array<string> option with get, set
    abstract protectedResourceMap: Map<string, array<string>> option with get, set

type Configuration = {
    auth      : AuthOptions
    cache     : CacheOptions option
    system    : SystemOptions option
    framework : FrameworkOptions option
    }