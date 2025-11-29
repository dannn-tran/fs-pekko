module FsPekko.Actor.Tests

open FsPekko.Actor
open FsPekko.Actor.ActorRef
open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

// https://pekko.apache.org/docs/pekko/current/typed/actors.html
module HelloWorld =
    type Greet = {
        Whom : string
        ReplyTo : ActorRef<Greeted>
    }
    and Greeted = {
        Whom: string
        From : ActorRef<Greet>
    }
    
    let Create: Behavior<Greet> = Behavior.Received (fun context message ->
        context.Log.Info($"Hello {message.Whom}!")
        message.ReplyTo <! { Whom = message.Whom; From = context.Self }
        Behavior.Same)
    
[<Test>]
let HelloWorld () =
    Assert.Pass()