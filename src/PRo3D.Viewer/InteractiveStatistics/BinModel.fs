namespace PRo3D.Viewer.InteractiveStatistics

open System
open Aardvark.Base
open Aardvark.UI
open FSharp.Data.Adaptive
open Adaptify

[<ModelType>]
type BinModel = 
    {    
        [<NonAdaptive>]
        id            : int
        count         : int 
        range         : Range1d
        annotationIDs : List<Guid>  //to keep track which annotations are responsible for the count       
    }   

module BinModel =

    let getBinMaxValue (bins:List<BinModel>) =
        bins |> List.map (fun b -> b.count) |> List.max




