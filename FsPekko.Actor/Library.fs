namespace FsPekko.Actor

type ActorRef<'T> =
    abstract member Tell: msg: 'T -> unit
    
module ActorRef =
    let inline (<!) (actorRef: ^T when ^T :> ActorRef<'U>) (msg: 'U) =
        actorRef.Tell msg

type ILogger =
    abstract member Info: string -> unit

type ActorContext<'T> =
    abstract member Spawn<'U>: Behavior<'U> -> name: string -> ActorRef<'U>
    abstract member Self: ActorRef<'T>
    abstract member Log: ILogger
and Behavior<'T> =
    | Same
    | Received of OnMessage: (ActorContext<'T> -> 'T -> Behavior<'T>)
    
    