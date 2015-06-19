#load "Rop.fs"

open Rop

type ErrorCode = DivideByZero | InvalidInt of string

let safeDiv a b =
    if b = 0 then Failure(DivideByZero) else Success(a / b)

let tryParse value =
    match System.Int32.TryParse value with
    | (true, result) -> Success(result)
    | _ -> Failure(InvalidInt value)

tryParse "10" <!> (*) 2 >>= safeDiv 100
//tryParse "10" |> map ((*) 2) >>= safeDiv 100
//tryParse "10" |> map ((*) 2) |> bind (safeDiv 100)
tryParse "0x" <!> (*) 2 >>= safeDiv 100