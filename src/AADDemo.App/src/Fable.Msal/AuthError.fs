// ts2fable 0.6.2
module rec AuthError
open System
open Fable.Core

let [<Import("AuthErrorMessage","module")>] AuthErrorMessage: TypeLiteral_02 = jsNative

type [<AllowNullLiteral>] IExports =
    abstract AuthError: AuthErrorStatic

/// General error class thrown by the MSAL.js library.
type [<AllowNullLiteral>] AuthError =
    abstract errorCode: string with get, set
    abstract errorMessage: string with get, set

/// General error class thrown by the MSAL.js library.
type [<AllowNullLiteral>] AuthErrorStatic =
    [<Emit "new $0($1...)">] abstract Create: errorCode: string * ?errorMessage: string -> AuthError
    abstract createUnexpectedError: errDesc: string -> AuthError

type [<AllowNullLiteral>] TypeLiteral_01 =
    abstract code: string with get, set
    abstract desc: string with get, set

type [<AllowNullLiteral>] TypeLiteral_02 =
    abstract unexpectedError: TypeLiteral_01 with get, set