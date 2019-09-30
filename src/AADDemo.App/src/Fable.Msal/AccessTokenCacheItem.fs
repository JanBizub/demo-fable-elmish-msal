// ts2fable 0.6.2
module rec AccessTokenCacheItem
open Fable.Core

type AccessTokenKey     = AccessTokenKey.AccessTokenKey
type AccessTokenValue   = AccessTokenValue.AccessTokenValue

type [<AllowNullLiteral>] IExports =
    abstract AccessTokenCacheItem: AccessTokenCacheItemStatic

type [<AllowNullLiteral>] AccessTokenCacheItem =
    abstract key: AccessTokenKey with get, set
    abstract value: AccessTokenValue with get, set

type [<AllowNullLiteral>] AccessTokenCacheItemStatic =
    [<Emit "new $0($1...)">] abstract Create: key: AccessTokenKey * value: AccessTokenValue -> AccessTokenCacheItem