module rec Fable.Msal
open Fable.Core
open Fable.Core.JsInterop
open Configuration

type UserAgentApplication = UserAgentApplication.UserAgentApplication

[<Import("UserAgentApplication","msal")>]
let userAgentApplication: UserAgentApplication.UserAgentApplicationStatic =
    jsNative

let initUserAgentApplication (configuration: Configuration) : UserAgentApplication.UserAgentApplication = importMember "./MSAL.js"