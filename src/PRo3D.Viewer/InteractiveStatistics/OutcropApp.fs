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
                    if (m.activeMeasurements |> IndexList.exists (fun _ v -> v = meas)) then m else  
                        let updatedAggregations = m.aggregations |> HashMap.map (fun _ v -> InteractiveStatisticsApp.update v msg)
                        let updatedActives = m.activeMeasurements |> IndexList.add meas
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
                let model = InteractiveStatisticsModel.createModel node groupsmodel.flat m.activeMeasurements
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

    //dropdown to select a type of measurement (e.g. dip/strike, length)               
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

    //headers
    //should be in the following format: "Aggregation | MEASUREMENT_1 | MEASUREMENT_2 |...| MEASUREMENT_N | Dropdown menu"
    let firstRow (titles : alist<Vis_Measurement>) = 
        let firstCol = [th [] [text "Aggregation"]] |> AList.ofList
        let headers = titles |> AList.map (fun m -> th [] [text (m.ToString())])
        let dropdown = [th [] [mTypeDropdown |> UI.map (fun f -> UpdateAllModels f)]] |> AList.ofList
        Incremental.tr AttributeMap.empty (AList.append (AList.append firstCol headers) dropdown)

    //create a table; each row = all visualisations for one InteractiveStatisticsModel
    let view (m : AdaptiveOutcropModel) =       
                         
        let first = [firstRow m.activeMeasurements] |> AList.ofList        
        
        let rows =
            m.aggregations
            |> AMap.toASet
            |> AList.ofASet
            |> AList.map (fun (id,model) -> (AnnotationStatisticsDrawings.view model m.activeMeasurements) |> UI.map (fun f -> InteractiveStatisticsMessage (id,f)))    

        Incremental.table 
            ([clazz "ui unstackable inverted table"] |> AttributeMap.ofList)
            (AList.append first rows)         
        
            
             
        
        
        
        
       




