// ts2fable 0.6.2
module rec Account
open System
open Fable.Core

type ClientInfo = ClientInfo.ClientInfo
type IdToken = IdToken.IdToken

type [<AllowNullLiteral>] IExports =
    abstract Account: AccountStatic

/// accountIdentifier       combination of idToken.uid and idToken.utid
/// homeAccountIdentifier   combination of clientInfo.uid and clientInfo.utid
/// userName                idToken.preferred_username
/// name                    idToken.name
/// idToken                 idToken
/// sid                     idToken.sid - session identifier
/// environment             idtoken.issuer (the authority that issues the token)
type [<AllowNullLiteral>] Account =
    abstract accountIdentifier: string with get, set
    abstract homeAccountIdentifier: string with get, set
    abstract userName: string with get, set
    abstract name: string with get, set
    abstract idToken: Object with get, set
    abstract sid: string with get, set
    abstract environment: string with get, set

/// accountIdentifier       combination of idToken.uid and idToken.utid
/// homeAccountIdentifier   combination of clientInfo.uid and clientInfo.utid
/// userName                idToken.preferred_username
/// name                    idToken.name
/// idToken                 idToken
/// sid                     idToken.sid - session identifier
/// environment             idtoken.issuer (the authority that issues the token)
type [<AllowNullLiteral>] AccountStatic =
    /// <summary>Creates an Account Object</summary>
    /// <param name="homeAccountIdentifier"></param>
    /// <param name="userName"></param>
    /// <param name="name"></param>
    /// <param name="idToken"></param>
    /// <param name="sid"></param>
    /// <param name="environment"></param>
    [<Emit "new $0($1...)">] abstract Create: accountIdentifier: string * homeAccountIdentifier: string * userName: string * name: string * idToken: Object * sid: string * environment: string -> Account
    /// <param name="idToken"></param>
    /// <param name="clientInfo"></param>
    abstract createAccount: idToken: IdToken * clientInfo: ClientInfo -> Account