// ts2fable 0.6.2
module rec IUri

type [<AllowNullLiteral>] IUri =
    abstract Protocol: string with get, set
    abstract HostNameAndPort: string with get, set
    abstract AbsolutePath: string with get, set
    abstract Search: string with get, set
    abstract Hash: string with get, set
    abstract PathSegments: ResizeArray<string> with get, set