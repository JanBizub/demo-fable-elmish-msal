module App

open System
open Elmish
open Elmish.Navigation
open Elmish.React
open Elmish.Debug
open Fable.React

type ArticleType =
  | BlogPost
  | News

type Article =
  {
    Id           : Guid
    Title        : string
    Content      : string
    ArticleType  : ArticleType
  }

type AppModel = 
  { Articles: Article list }
  
  static member InitArticles () =
    [
      { Id = Guid.NewGuid(); Title = "F# Computation Expressions"; Content = "Super Feature!"; ArticleType = ArticleType.BlogPost }
      { Id = Guid.NewGuid(); Title = "Dignosing your BMW e65"; Content = "Get yourself ISTA/D first"; ArticleType = ArticleType.BlogPost }
      { Id = Guid.NewGuid(); Title = "Functional programming"; Content = "More fun than oop"; ArticleType = ArticleType.BlogPost }
      { Id = Guid.NewGuid(); Title = "Fable App and AAD"; Content = "Can be done. Use MSAL"; ArticleType = ArticleType.BlogPost }
      { Id = Guid.NewGuid(); Title = "Fable App and Keycloak"; Content = "Of course"; ArticleType = ArticleType.BlogPost }
    ]
  static member Initial =
    { Articles = AppModel.InitArticles () }

type Msg =
  | Hello


let update msg model =
  match msg with
  | Hello -> model, []
  
let init a = 
 AppModel.Initial, []

open EditorComponent.Types
open Fable.React.Props


let articleToEditorArticle (article: Article) =
  {Id = article.Id; Title = article.Title; Content = article.Content} : EditorArticle

let appView model dispatch =
  div [Class "content"] [
    
    div [Class "row col-md-12 justify-content-center"] [
      h1 [Class ""] ["Elmish Composition Example" |> str]      
    ]
    
    div [Class "row"] [
      div [Class "col-md-2"] [
        p [] ["menu" |> str]
      ]
      
      div [Class "col-md-10"] (
        model.Articles
        |> List.map (fun article -> article |> articleToEditorArticle |> EditorComponent.View.render))
    ]
  ]
  

Program.mkProgram init update appView
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "aad-demo-app"
//#if DEBUG
|> Program.withDebugger
//#endif
|> Program.run