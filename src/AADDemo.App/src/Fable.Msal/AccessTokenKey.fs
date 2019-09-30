// ts2fable 0.6.2
module rec AccessTokenKey
open Fable.Core

type [<AllowNullLiteral>] IExports =
    abstract AccessTokenKey: AccessTokenKeyStatic

type [<AllowNullLiteral>] AccessTokenKey =
    abstract authority: string with get, set
    abstract clientId: string with get, set
    abstract scopes: string with get, set
    abstract homeAccountIdentifier: string with get, set

type [<AllowNullLiteral>] AccessTokenKeyStatic =
    [<Emit "new $0($1...)">] abstract Create: authority: string * clientId: string * scopes: string * uid: string * utid: string -> AccessTokenKey