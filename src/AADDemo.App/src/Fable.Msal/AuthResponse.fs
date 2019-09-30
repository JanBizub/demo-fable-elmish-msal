// ts2fable 0.6.2
module rec AuthResponse
open System

type Account = Account.Account
type IdToken = IdToken.IdToken

type [<AllowNullLiteral>] IExports =
    abstract buildResponseStateOnly: state: string -> AuthResponse

type AuthResponse = 
  { uniqueId      : string 
    tenantId      : string 
    tokenType     : string
    idToken       : IdToken
    accessToken   : string 
    scopes        : array<string>
    expiresOn     : DateTime
    account       : Account
    accountState  : string }