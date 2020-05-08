namespace EditorComponent
open System

module Types =
  type EditorArticle = {
    Id      : Guid
    Title   : string
    Content : string
  }
  
  type EditorState =
    | Read
    | Edit
    | Saving
    | Failed of exn
    
  type EditorProps = {
    EditorState : EditorState
    Article     : EditorArticle
  }
  
  type ExternamMsg =
    | NoOp
    | ChangeArticle of string
  
  type EditorMsg =
    | EditArticle
    | CloseArticle
    | SaveArticle
    | SaveSuccess of EditorArticle
    | SaveError of exn

  
module State =
  open Elmish
  open Types
  
  let init (article: EditorArticle): EditorProps * Cmd<EditorMsg> =
    {EditorState = Read; Article = article}, Cmd.none
    
  let update msg model: EditorProps * Cmd<EditorMsg> =
    match msg with
    | EditArticle ->
      model, []
    | CloseArticle ->
      model, []
    | SaveArticle ->
      model, []
    | SaveSuccess savedArticle ->
      model, []
    | SaveError error ->
      model, []
    

module View =
  open Types
  open State
  open Elmish
  open Fable.React
  open Fable.React.Props
  
  let (?) = Fable.Core.JsInterop.op_Dynamic
  let updateField updateFunction (event: Browser.Types.Event) = 
    let cachedEventVal = event.target?value.ToString()
    cachedEventVal |> string |> updateFunction
  
  let private editorView state dispatch =
    
    let titleState = Hooks.useState(state.Article.Title)
    let titleInput = input [
      Class        "form-control"
      Type         "Text"
      DefaultValue state.Article.Title
      OnChange     (updateField titleState.update)
    ]
    
    let contentState = Hooks.useState(state.Article.Title)
    let contentInput =
      textarea [
      Class        "form-control"
      Type         "Text"
      Rows         10
      DefaultValue state.Article.Content
      OnChange     (updateField contentState.update)
      ] []
    
    let saveButton =
      button [Class "btn btn-success"] ["Save" |> str]
      
    let closeButton =  
      button [Class "btn btn-warning"] ["Close" |> str]
    
    div [] [
      titleInput
      contentInput
      div [Class "btn-group"] [saveButton;closeButton]
      hr []
    ]
  
  open Feliz
  open Feliz.ElmishComponents
  
  let render article = 
    React.elmishComponent(
      name  = sprintf "articleEditor_%A" article.Id,
      init  = init article,
      update = update,
      render = editorView
    )