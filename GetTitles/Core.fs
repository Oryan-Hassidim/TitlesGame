namespace GetTitles

open System
open FSharp.Data
open System.Collections.Generic




module Core =
    type item =
        { title: string
          link: string
          description: string option
          pubDate: DateTime
          publisher: int }
    module publishers =
        let private publishers = System.Collections.Generic.Dictionary<int, string>()
        let add id name = publishers.Add(id, name)
        let get id =
            match publishers.TryGetValue(id) with
            | true, name -> Some name
            | _ -> None
        
    
