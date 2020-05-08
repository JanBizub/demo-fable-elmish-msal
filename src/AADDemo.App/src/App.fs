module App

open Elmish
open Elmish.Navigation
open Elmish.React
open Elmish.Debug
open Fable.React

type AppModel = 
  { isUiLoading : bool
    nameFromAPI : string }
  static member Empty =
    { isUiLoading = false
      nameFromAPI = "" }

type Msg =
  | Hello



let update msg model =
  match msg with
  | Hello -> model, []
  
let init a = 
 AppModel.Empty, []
 
 

let appView model dispatch =
  div [] [
    "Hello" |> str    
  ]
  
  
  

Program.mkProgram init update appView
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
//#if DEBUG
|> Program.withDebugger
//#endif
|> Program.run