// ts2fable 0.6.2
module rec AuthenticationParameters
open System
open Fable.Core

type Account = Account.Account

type [<AllowNullLiteral>] IExports =
    abstract validateClaimsRequest: request: AuthenticationParameters -> unit

type [<AllowNullLiteral>] QPDict =
    [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> string with get, set

type AuthenticationParameters = {
      scopes                  : array<string> option
      extraScopesToConsent    : array<string> option
      prompt                  : string option 
      extraQueryParameters    : QPDict option 
      claimsRequest           : string option 
      authority               : string option 
      state                   : string option 
      correlationId           : string option 
      account                 : Account option
      sid                     : string option 
      loginHint               : string option 
    }