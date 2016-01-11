let safeDiv a b =
    if b = 0 then None else Some(a / b)

let tryParse value =
    match System.Int32.TryParse value with
    | (true, result) -> Some(result)
    | _ -> None

// nie trzeba - jest już w bib. standardowej
//module Option
//    let bind f = function
//       | None -> None
//       | Some x -> f x

type OptionBuilder() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) = Some x

let option = new OptionBuilder()

let divideByWorkflow astring bstring =
    option {
        let! a = tryParse astring
        let! b = tryParse bstring
        let! c = safeDiv a b
        return c * 2
    }

divideByWorkflow "10" "5"
divideByWorkflow "bla bla" "5"
divideByWorkflow "10" "foo"
divideByWorkflow "0" "5"
divideByWorkflow "10" "0"
















let (>>=) m f = Option.bind f m
let (<!>) m f = Option.map f m

tryParse "10"
>>= fun a -> tryParse "5" >>= safeDiv a
<!> (*) 2