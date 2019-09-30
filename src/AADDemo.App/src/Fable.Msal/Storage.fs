// ts2fable 0.6.2
module rec Storage
open System
open Fable.Core

type AccessTokenCacheItem = AccessTokenCacheItem.AccessTokenCacheItem
type CacheLocation        = Configuration.CacheLocation

type [<AllowNullLiteral>] IExports =
    abstract Storage: StorageStatic

type [<AllowNullLiteral>] Storage =
    abstract setItem: key: string * value: string * ?enableCookieStorage: bool -> unit
    abstract getItem: key: string * ?enableCookieStorage: bool -> string
    abstract removeItem: key: string -> unit
    abstract clear: unit -> unit
    abstract getAllAccessTokens: clientId: string * homeAccountIdentifier: string -> array<AccessTokenCacheItem>
    abstract removeAcquireTokenEntries: ?state: string -> unit
    abstract resetCacheItems: unit -> unit
    abstract setItemCookie: cName: string * cValue: string * ?expires: float -> unit
    abstract getItemCookie: cName: string -> string
    abstract getCookieExpirationTime: cookieLifeDays: float -> string
    abstract clearCookie: unit -> unit

type [<AllowNullLiteral>] StorageStatic =
    [<Emit "new $0($1...)">] abstract Create: cacheLocation: CacheLocation -> Storage
    /// <summary>Create acquireTokenAccountKey to cache account object</summary>
    /// <param name="accountId"></param>
    /// <param name="state"></param>
    abstract generateAcquireTokenAccountKey: accountId: obj option * state: string -> string
    /// <summary>Create authorityKey to cache authority</summary>
    /// <param name="state"></param>
    abstract generateAuthorityKey: state: string -> string