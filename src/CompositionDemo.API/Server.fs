open System.IO
open System
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Microsoft.WindowsAzure.Storage

type Person = {
  Name     : string
  Surname  : string
}

let tryGetEnv key =
  match Environment.GetEnvironmentVariable key with
  | x when String.IsNullOrWhiteSpace x -> None
  | x -> Some x

let publicPath =
  tryGetEnv "public_path" |> Option.defaultValue "../Client/public" |> Path.GetFullPath
let storageAccount =
  tryGetEnv "STORAGE_CONNECTIONSTRING"
  |> Option.defaultValue "UseDevelopmentStorage=true"
  |> CloudStorageAccount.Parse

open Saturn

let port =
  "SERVER_PORT"
  |> tryGetEnv
  |> Option.map uint16
  |> Option.defaultValue 8085us

let webApp = router {
  
  get "/api/init" (fun next ctx ->
    task {
      let counter = 10
      return! json counter next ctx
    })
  
  post "/api/person" (fun next ctx ->
    task {
      let person = ctx.BindModelAsync<Person> // how do I do post?
      return! json person next ctx    
    })
}

let configureAzure (services:IServiceCollection) =
  tryGetEnv "APPINSIGHTS_INSTRUMENTATIONKEY"
  |> Option.map services.AddApplicationInsightsTelemetry
  |> Option.defaultValue services

let app = application {
  url ("http://0.0.0.0:" + port.ToString() + "/")
  use_router webApp
  memory_cache
  use_static publicPath
  use_json_serializer(Thoth.Json.Giraffe.ThothSerializer())
  service_config configureAzure
  use_gzip
}

run app
