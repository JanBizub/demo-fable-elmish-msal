namespace AADDemo.API2.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Cors

[<Authorize>]
[<EnableCors("AllowAllCors")>]
[<Route("[controller]")>]
type CarController () =
    inherit ControllerBase()

    [<HttpGet>]
    member __.Get() = "Honda Legend" |> fsharpJson