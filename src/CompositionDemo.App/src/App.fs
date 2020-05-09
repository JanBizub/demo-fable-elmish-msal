module App

module Types =
  open SharedTypes
  open MenuComponent.Types

  type State = {
    Menu     : MenuComponent.Types.State
  }

  type Msg =
    | Hello
    | MenuMsg of MenuComponent.Types.MenuMsg

module State = 
  open Types

  let update msg model =
    match msg with
    | Hello -> model, []
    
  let init a = { Menu = MenuComponent.Types.State.Empty }, []

module View =
  open Fable.React
  open Fable.React.Props

  let render (model: Types.State) dispatch =
    div [Class "content"] [
      
      div [Class "row col-md-12 justify-content-center"] [
        h1 [Class ""] ["Elmish Composition Example" |> str]      
      ]
      
      div [Class "row"] [ MenuComponent.View.render model.Menu (Types.MenuMsg >> dispatch) ]
    ]
    
  
open System
open Elmish
open Elmish.Navigation
open Elmish.React
open Elmish.Debug

Program.mkProgram State.init State.update View.render
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "aad-demo-app"
//#if DEBUG
|> Program.withDebugger
//#endif
|> Program.run