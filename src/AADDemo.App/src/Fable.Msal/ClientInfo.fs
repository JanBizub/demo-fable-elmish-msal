// ts2fable 0.6.2
module rec ClientInfo
open Fable.Core

type [<AllowNullLiteral>] IExports =
    abstract ClientInfo: ClientInfoStatic

type [<AllowNullLiteral>] ClientInfo =
    abstract uid: string with get, set
    abstract utid: string with get, set

type [<AllowNullLiteral>] ClientInfoStatic =
    [<Emit "new $0($1...)">] abstract Create: rawClientInfo: string -> ClientInfo