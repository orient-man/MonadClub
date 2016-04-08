// Railway Oriented Programming
// See: http://fsharpforfunandprofit.com/posts/recipe-part2/
module Rop

type Result<'TSuccess, 'TFailure> =
    | Success of 'TSuccess
    | Failure of 'TFailure

let bind f m =
    match m with Success x -> f x | _ -> m

let map f m =
    match m with Success x -> Success (f x) | _ -> m

// 1)
let (>>=) m f = bind f m

let (<!>) m f = map f m

// 2)
type EitherBuilder () =
    member this.Bind(x, f) = bind f x
    member this.Return x = Success x
