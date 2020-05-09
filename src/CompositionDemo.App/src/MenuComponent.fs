namespace MenuComponent

module Types =
  open System
  open SharedTypes

  type State = 
    { 
      Articles       : Article list
      ArticlesEdited : Article list
      ArticlesSaving : Article list 
    }
    static member Empty = {Articles = []; ArticlesEdited = []; ArticlesSaving = []}

  type MenuMsg =
    | EditArticle  of Guid
    | CloseArticle of Guid
    | SaveArticle  of Article

module View =
  open Fable.React
  open Fable.React.Props

  let render model dispatch =
    div [Class "content"] [
      
      div [Class "row col-md-12 justify-content-center"] [
        h1 [Class ""] ["Elmish Composition Example" |> str]      
      ]
      
      div [Class "row"] [
        div [Class "col-md-2"] [
          p [] ["menu" |> str]
        ]
        
        div [Class "col-md-10"] [
          p [] ["Pbses" |> str]
        ]
      ]
    ]