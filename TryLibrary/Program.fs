open GetTitles
open Now14
open System.Threading.Tasks
open System

module Program =
    let _main argv =
        let items = (Now14.items ()).Result
        printfn "%A" items
        0 // return an integer exit code

    [<EntryPoint>]
    let main argv = _main argv
