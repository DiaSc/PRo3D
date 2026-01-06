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
        | UpdateAllModels (msg) ->
            match msg with
            | CreateVisualization meas -> 
                if (m.aggregations |> HashMap.isEmpty) then m else
                    if (m.activeMeasurements |> List.contains meas) then m else  
                        let updatedAggregations = m.aggregations |> HashMap.map (fun _ v -> InteractiveStatisticsApp.update v msg)
                        let updatedActives = m.activeMeasurements |> List.append [meas]
                        {m with aggregations = updatedAggregations; activeMeasurements = updatedActives}
            | _ -> let updatedAggregations = m.aggregations |> HashMap.map (fun _ v -> InteractiveStatisticsApp.update v msg)
                   {m with aggregations = updatedAggregations}            
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
                
    //let viewRows ()

    let mTypeDropdown =        
        div [ clazz "ui menu"; style "width:150px; height:20px;padding:0px; margin:0px"] [
            onBoot "$('#__ID__').dropdown('on', 'hover');" (
                div [ clazz "ui dropdown item"; style "width:100%"] [
                    text "Measurement"
                    i [clazz "dropdown icon"; style "margin:0px 5px"] [] 
                    div [ clazz "ui menu"] [
                        div [clazz "ui inverted item"; onMouseClick (fun _ -> CreateVisualization Vis_Measurement.LENGTH)] [text "Length"]
                        div [clazz "ui inverted item"; onMouseClick (fun _ -> CreateVisualization Vis_Measurement.DIP_AZIMUTH)] [text "Dip Azimuth"]
                        div [clazz "ui inverted item"; onMouseClick (fun _ -> CreateVisualization Vis_Measurement.STRIKE_AZIMUTH)] [text "Strike Azimuth"]                         
                    ]
                ]
            )
        ] 

    let view (m:AdaptiveOutcropModel) =        
                         
        let style' = "color: white; font-family:Consolas;" 

         
        
        Html.table [
            Html.row "Aggregation" [mTypeDropdown |> UI.map (fun f -> UpdateAllModels f)]
            

        ]

        //Incremental.div (AttributeMap.ofList [style style']) 
        //    (
        //        m.aggregations
        //        |> AMap.map (fun k v ->
        //            div[style "float:left"][AnnotationStatisticsDrawings.view v |> UI.map (fun f -> InteractiveStatisticsMessage (k,f))])
        //        |> AMap.toASet
        //        |> ASet.toAList 
        //        |> AList.map(fun (a,b) -> b)

        //    )
            
             
        
        
        
        
       




