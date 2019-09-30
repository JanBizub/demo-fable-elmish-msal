// ts2fable 0.6.2
module rec AccessTokenValue
open Fable.Core

type [<AllowNullLiteral>] IExports =
    abstract AccessTokenValue: AccessTokenValueStatic

type [<AllowNullLiteral>] AccessTokenValue =
    abstract accessToken: string with get, set
    abstract idToken: string with get, set
    abstract expiresIn: string with get, set
    abstract homeAccountIdentifier: string with get, set

type [<AllowNullLiteral>] AccessTokenValueStatic =
    [<Emit "new $0($1...)">] abstract Create: accessToken: string * idToken: string * expiresIn: string * homeAccountIdentifier: string -> AccessTokenValue