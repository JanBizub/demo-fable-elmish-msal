module SharedTypes
open System

type Article = {
  Id      : Guid
  Title   : string
  Content : string   
}