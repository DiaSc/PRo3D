namespace PRo3D.Viewer.InteractiveStatistics

open System
open PRo3D.Base
open PRo3D.Base.Annotation
open Aardvark.Base
open Aardvark.UI
open Aardvark.UI.Primitives
open PRo3D.Core
open FSharp.Data.Adaptive

module OutcropApp =

    let update (m : OutcropModel) (act : OutcropAction) =
        match act with
        | InteractiveStatisticsMessage (id,msg) -> 
           let updatedAggregations = m.aggregations |> HashMap.alter id (
            fun o -> 
            match o with
            | Some model -> Some(InteractiveStatisticsApp.update model msg)
            | None -> None            
           )
           let hovered = 
                match (updatedAggregations |> HashMap.tryFind id) with
                | Some model -> 
                    match model.hoveredLeaves with
                    | Some list -> Some id
                    | None -> None
                | None -> None
           {m with aggregations = updatedAggregations; activeAggregation = hovered}          

        | CreateAggregation (node, groupsmodel) -> 
            //check if the aggregation already exists (if a user clicks the aggregation button again)            
            if m.aggregations.ContainsKey node.key then
                Log.line "Aggregation for this node already exists."
                m
            else
                let model = InteractiveStatisticsModel.createModel node groupsmodel.flat
                let map = m.aggregations.Add (node.key, model)
                {m with aggregations = map}

        //| UpdateAggregation (id, act) -> m //TODO

        //TODO: if a node higher up in the hierarchy is removed (i.e. it had subnodes) then the aggregations of the subnodes will also be deleted
        | RemoveAggregation (id) -> 
            let map = m.aggregations.Remove id
            {m with aggregations = map}

    let getHoveredAnnos (m : OutcropModel) (aggID : Guid) =
        match (m.aggregations |> HashMap.tryFind aggID) with
                | Some model -> model.hoveredLeaves                    
                | None -> None            

    let view (m:AdaptiveOutcropModel) =        
                         
        let style' = "color: white; font-family:Consolas;"        

        Incremental.div (AttributeMap.ofList [style style']) 
            (
                m.aggregations
                |> AMap.map (fun k v ->
                    div[style "float:left"][AnnotationStatisticsDrawings.view v |> UI.map (fun f -> InteractiveStatisticsMessage (k,f))])
                |> AMap.toASet
                |> ASet.toAList 
                |> AList.map(fun (a,b) -> b)

            )
            
             
        
        
        
        
       




