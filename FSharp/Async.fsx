open System.IO
open System.Net

let compute5 = async { return 5 }
let compute7 = async { return 7 }
let add x y = async { return x + y }

let compute = async {
    let! a = compute5
    let! b = compute7
    let! c = add a b
    return c * 2
}

Async.RunSynchronously compute

let downloadUrl(url : string) = async {
    let request = HttpWebRequest.Create(url)
    use! response = request.AsyncGetResponse()
    let stream = response.GetResponseStream()
    use reader = new StreamReader(stream)
    return! reader.ReadToEndAsync() |> Async.AwaitTask
}

let downloadUrl_debunked(url : string) = 
    async.Delay(fun () ->
        let request = HttpWebRequest.Create(url)
        async.Bind(request.AsyncGetResponse(), fun response ->
            async.Using(response, fun response ->
                let stream = response.GetResponseStream()
                async.Using(new StreamReader(stream), fun reader ->
                    reader.ReadToEndAsync() |> Async.AwaitTask))))