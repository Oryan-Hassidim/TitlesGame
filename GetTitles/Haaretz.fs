namespace GetTitles

open System
open GetTitles
open FSharp.Data
open Core

module Haaretz =
    publishers.add 1 "haaretz"
    type Rss = XmlProvider<"https://www.haaretz.co.il/srv/rss---feedly">

    let items () =
        task {
            let feedlyRss =
                Rss
                    .Load(
                        "https://www.haaretz.co.il/srv/rss---feedly"
                    )
                    .Channel
                    .Items

            let opinionRss =
                Rss
                    .Load(
                        "https://www.haaretz.co.il/srv/rss-opinion"
                    )
                    .Channel
                    .Items

            return
                Seq.concat [ feedlyRss; opinionRss ]
                |> Seq.map (fun i ->
                    { title = i.Title
                      link = i.Link
                      description = i.Description
                      pubDate = i.PubDate.DateTime
                      publisher = 1 })
                |> Seq.toList
        }
