// Railway Oriented Programming
// See: http://fsharpforfunandprofit.com/posts/recipe-part2/
module Rop

type Result<'TSuccess, 'TFailure> =
    | Success of 'TSuccess
    | Failure of 'TFailure

let bind switchFunction twoTrackInput =
    match twoTrackInput with
    | Success s -> switchFunction s
    | Failure f -> Failure f

let map oneTrackFunction twoTrackInput =
    match twoTrackInput with
    | Success s -> Success (oneTrackFunction s)
    | Failure f -> Failure f

// 1)
let (>>=) twoTrackInput switchFunction =
    bind switchFunction twoTrackInput

let (<!>) m f = map f m

// 2)
type EitherBuilder () =
    member this.Bind(x, f) = bind f x
    member this.Return x = Success x
