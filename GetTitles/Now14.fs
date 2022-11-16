namespace GetTitles

open System
open GetTitles
open FSharp.Data
open Core

module Now14 =
    publishers.add 2 "Now 14"

    type Html =
        HtmlProvider<"https://www.now14.co.il/category/c/%d7%a4%d7%95%d7%9c%d7%99%d7%98%d7%99-%d7%9e%d7%93%d7%99%d7%a0%d7%99/">

    let contentZoneToItem (node: HtmlNode) =
        let elements = node.Elements()
        let h2Elements = elements[ 0 ].Elements()
        let title = elements[ 0 ].InnerText()
        let description = elements[ 1 ].InnerText()
        let link = h2Elements[ 0 ].AttributeValue "href"

        let date =
            let items = List.last(elements).InnerText().Split(" | ")
            let date = DateOnly.ParseExact(items[1], "dd/MM")
            let time = TimeOnly.ParseExact(items[2], "H:mm")
            DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second)

        { title = title
          link = link
          description = Some description
          pubDate = date
          publisher = 2 }


    let items () =
        task {
            let! html1 =
                Html.AsyncLoad
                    "https://www.now14.co.il/category/c/%d7%a4%d7%95%d7%9c%d7%99%d7%98%d7%99-%d7%9e%d7%93%d7%99%d7%a0%d7%99/"

            let! html2 = Html.AsyncLoad "https://www.now14.co.il/category/c/%d7%93%d7%a2%d7%95%d7%aa/"

            return
                html1.Html.CssSelect "div.archive-post-unit .content-zone"
                |> List.append (html2.Html.CssSelect "div.archive-post-unit .content-zone")
                |> List.map contentZoneToItem
        }
