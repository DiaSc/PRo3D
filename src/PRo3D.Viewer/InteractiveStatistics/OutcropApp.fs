namespace PRo3D.Viewer.InteractiveStatistics

open System
open PRo3D.Base
open PRo3D.Base.Annotation
open Aardvark.Base
open Aardvark.UI
open PRo3D.Core
open FSharp.Data.Adaptive

module OutcropApp =

    let update (m : OutcropModel) (act : OutcropAction) =
        match act with
        | InteractiveStatisticsMessage msg -> m

        | CreateAggregation (node, groupsmodel) ->            
            let model = InteractiveStatisticsModel.createModel node groupsmodel.flat
            let map = m.aggregations.Add (node.key, model)
            {m with aggregations = map}

        //| UpdateAggregation (id, act) -> m //TODO

        //TODO: if a node higher up in the hierarchy is removed (i.e. it had subnodes) then the aggregations of the subnodes will also be deleted
        | RemoveAggregation (id) -> 
            let map = m.aggregations.Remove id
            {m with aggregations = map}
            

    let view (m:AdaptiveOutcropModel) =        
                         
        let style' = "color: white; font-family:Consolas;"         
        //let description = m.node.name     
        
        let test = m.aggregations |> AMap.map (fun k v -> AnnotationStatisticsDrawings.view v) |> AMap.toASet |> ASet.toAList |> AList.map(fun (a,b) -> b)
             
        Incremental.div (AttributeMap.ofList [style style']) (test) |> UI.map InteractiveStatisticsMessage




