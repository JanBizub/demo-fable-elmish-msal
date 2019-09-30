[<AutoOpen>]
module WebApiHelpers
open Microsoft.AspNetCore.Mvc
open Newtonsoft.Json

let fsharpJson data =
  let jsonConverter = Fable.JsonConverter() :> JsonConverter
  JsonConvert.SerializeObject(data, [|jsonConverter|])