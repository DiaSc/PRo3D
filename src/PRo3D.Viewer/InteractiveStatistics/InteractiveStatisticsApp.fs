namespace PRo3D.Viewer.InteractiveStatistics

open System
open PRo3D.Base
open PRo3D.Base.Annotation
open Aardvark.Base
open Aardvark.UI
open Aardvark.UI.Primitives
open PRo3D.Core
open FSharp.Data.Adaptive
//open PRo3D.Viewer.InteractiveStatistics

module InteractiveStatisticsApp =         
    

    let update (m:InteractiveStatisticsModel) (a:InteractiveStatisticsAction) =
        match a with
        | SetActive ->  {m with active = true}
        | AddAnnotation (annos) -> 
            let leaves' = annos |> HashMap.ofList |> HashMap.union m.leaves 

            let vis' = 
                m.visualisations 
                |> HashMap.map (fun m model -> 
                    let data' = StatisticsVisualizationModel.getVisualizationData (leaves' |> HashMap.toList) m
                    let model' = 
                        match model with
                        | Histogram h -> StatisticsVisualization_App.update model (HistogramMessage (UpdateData data'))
                        | RoseDiagram r -> StatisticsVisualization_App.update model (RoseDiagramMessage (UpdateRD data'))
                    model'
                )
            {m with leaves = leaves'; visualisations = vis'}
        
        | RemoveAnnotation (id) -> 
            let leaves' = m.leaves |> HashMap.remove id

            //update the data of all visualisations
            let leavesList = leaves' |> HashMap.toList
            let vis' = 
                m.visualisations 
                |> HashMap.map (fun m model -> 
                    let data' = StatisticsVisualizationModel.getVisualizationData leavesList m
                    let model' = 
                        match model with
                        | Histogram h -> StatisticsVisualization_App.update model (HistogramMessage (UpdateData data'))
                        | RoseDiagram r -> StatisticsVisualization_App.update model (RoseDiagramMessage (UpdateRD data'))
                    model'
                )
            {m with leaves = leaves'; visualisations = vis'}

        | StartPeek (anno) ->          
            let vis' = 
                m.visualisations 
                |> HashMap.map (fun meas model -> 
                    let data = StatisticsVisualizationModel.getVisualizationData [(anno.key,anno)] meas |> List.first
                    match data with
                    | Some d -> 
                        let d' = d |> snd
                        let model' = 
                            match model with
                            | Histogram h -> StatisticsVisualization_App.update model (HistogramMessage (PeekBinStart d'))
                            | RoseDiagram r -> StatisticsVisualization_App.update model (RoseDiagramMessage (PeekRDBinStart d'))
                        model'
                    | None -> model
                    
                )
            {m with visualisations = vis'}

        | EndPeek ->
            let vis' = 
                m.visualisations 
                |> HashMap.map (fun meas model ->                     
                        let model' = 
                            match model with
                            | Histogram h -> StatisticsVisualization_App.update model (HistogramMessage (PeekBinEnd))
                            | RoseDiagram r -> StatisticsVisualization_App.update model (RoseDiagramMessage (PeekRDBinEnd))
                        model'                   
                )
            {m with visualisations = vis'}



        | CreateVisualization (measurement) ->             
            let vis = StatisticsVisualizationModel.createVisualization (m.leaves |> HashMap.toList) measurement
            let visList = m.visualisations |> HashMap.add measurement vis
            {m with visualisations = visList}
        | StatisticsVisualizationMessage (id,msg) ->   
            let updatedVisualizations = m.visualisations |> HashMap.alter id (
                fun o -> 
                    match o with
                    | Some model -> Some(StatisticsVisualization_App.update model msg)
                    | None -> None            
                )
            //check if a diagram is hovered
            let hovering = 
                match (updatedVisualizations |> HashMap.tryFind id) with
                | Some model -> 
                    match model.hoveringActive with
                    | Some id -> StatisticsVisualization_App.getHoveredIDs model id
                    | None -> None
                | None -> None
            {m with visualisations = updatedVisualizations; hoveredLeaves = hovering}  
        | UpdateAllVisualizations (ranges) ->
            let visualizations' =
                m.visualisations
                |> HashMap.map (fun meas model -> 
                    let range = ranges |> HashMap.tryFind meas
                    match range with
                    | Some r -> StatisticsVisualization_App.update model (SetRange r)
                    | None -> model
                )
            {m with visualisations = visualizations'}           

          
       
  
    //let rec update (m:InteractiveStatisticsModel) (a:AnnoStatsAction) =
    //    match a with
    //    //Add selection if not in the list, remove selection if already in the list
    //    | UpdateSingleSelectedAnnotation (id, g) ->         
    //        match (g.flat |> HashMap.tryFind id) with
    //        | Some l ->                 
    //            let updatedAnnotations = 
    //                match (m.selectedAnnotations |> HashMap.tryFind id) with
    //                | Some _ -> m.selectedAnnotations.Remove id
    //                | None -> m.selectedAnnotations.Add (id,Leaf.toAnnotation l)
                
    //            if (updatedAnnotations |> HashMap.isEmpty) then (update m Reset)
    //            else
    //                let updatedMeasurements =
    //                    let updatedData = 
    //                        m.properties
    //                        |> HashMap.map (fun info _ -> getMeasurementData info (updatedAnnotations |> HashMap.toList))
    //                    StatisticsMeasurement_App.update' m.properties updatedData
    //                {m with selectedAnnotations = updatedAnnotations; properties = updatedMeasurements}
    //        | None -> m
    //    | UpdateMultipleSelectedAnnotations g ->
    //        let selected = g.selectedLeaves |> HashSet.map (fun selection -> selection.id) |> HashSet.toList
    //        let updatedAnnotations = 
    //            selected 
    //            |> List.map (fun id -> 
    //                match (g.flat |> HashMap.tryFind id) with
    //                | Some leaf -> Some(id,Leaf.toAnnotation leaf)
    //                | None -> None
    //            )
    //            |> List.choose (fun entry -> entry)                

    //        if (updatedAnnotations |> List.isEmpty) then (update m Reset)
    //        else
    //            let updatedMeasurements =
    //                let updatedData = 
    //                    m.properties
    //                    |> HashMap.map (fun info _ -> getMeasurementData info updatedAnnotations)
    //                StatisticsMeasurement_App.update' m.properties updatedData
    //            {m with selectedAnnotations = (updatedAnnotations |> HashMap.ofList); properties = updatedMeasurements}     
    //    | MeasurementMessage (mType,act) ->
    //        match (m.properties.TryFind mType) with
    //        |Some p -> 
    //            let updatedMeasurement = StatisticsMeasurement_App.update p act
    //            let updatedPropList = 
    //                m.properties 
    //                |> HashMap.alter mType (function None -> None | Some _ -> Some updatedMeasurement)
    //            {m with properties = updatedPropList}
    //        |None -> m            
    //    | AddNewMeasurement mType -> 
    //        match (m.properties.ContainsKey mType) with
    //        | true -> m
    //        | false -> 
    //            let data = getMeasurementData mType (m.selectedAnnotations |> HashMap.toList)
    //            let newMeasurement = StatisticsMeasurementModel.init data mType
    //            let properties = m.properties.Add (mType, newMeasurement)
    //            {m with properties = properties}
    //    | PredictStart annotation ->
    //        match (m.properties.IsEmpty) with
    //        | true -> m               
    //        | false ->                 
    //            let peekValues =
    //                m.properties |> HashMap.map (fun mType _ ->
    //                    let res = getMeasurementData mType [annotation.key,annotation]
    //                    res.Head |> snd
    //                 )
    //            let updatedMeasurements = StatisticsMeasurement_App.startPeekForEachMeasurement m.properties peekValues
    //            {m with properties = updatedMeasurements}   
    //    | PredictEnd ->
    //        let updatedMeasurements = StatisticsMeasurement_App.endPeekForEachMeasurement m.properties
    //        {m with properties = updatedMeasurements}  
    //    | Reset -> 
    //        {m with selectedAnnotations = HashMap.empty; properties = HashMap.empty}
    //    | DeleteMeasurement mType ->
    //        match (m.properties.ContainsKey mType) with
    //        | true -> 
    //            let properties = m.properties.Remove mType
    //            {m with properties = properties}
    //        | false -> m         
            
    //    | CreateRDFromGroup (node, g) ->
                        
    //        let annotations = 
    //            let test = node.leaves |> IndexList.toList
    //            test |> List.map (fun id -> 
    //                match (g.flat |> HashMap.tryFind id) with
    //                | Some leaf -> Some(id,Leaf.toAnnotation leaf)
    //                | None -> None
    //            )
    //            |> List.choose (fun entry -> entry) 
                       
            
    //        update {m with selectedAnnotations = (annotations |> HashMap.ofList)} (AddNewMeasurement (StatisticsMeasurementModel.initMeasurementType Kind.DIP_AZIMUTH Scale.Angular))
            
            
            //TODO
            //{m with selectedAnnotations = (updatedAnnotations |> HashMap.ofList)}



module AnnotationStatisticsDrawings =
    
    let view (m : AdaptiveInteractiveStatisticsModel) (measurements : alist<Vis_Measurement>) =   

        //name of the node and number of annotations in it
        let firstCell = 
            let s = AVal.map2 (fun x y -> sprintf "%A | N: %A" x y) m.node.name (m.leaves |> AMap.count)
            let it = Incremental.text s                        
            [td [] [it]] |> AList.ofList
        
        //all visualisations
        let cells = 
            measurements 
            |> AList.map (fun meas -> 
                let vis = m.visualisations |> AMap.tryFind meas |> AVal.force
                match vis with
                | Some v -> td [] [StatisticsVisualization_App.drawVisualization2 v (new V2i(300, 150)) |> UI.map (fun f -> StatisticsVisualizationMessage (meas,f))]
                | None -> td [] []  
            )                     

        Incremental.tr AttributeMap.empty (AList.append firstCell cells)


             
        
        
        








    //    let s = selectedAnnos |> AMap.isEmpty        

    //    Incremental.div (AttributeMap.ofList [style style']) (           
    //            alist {               
    //                let! empty = s
    //                match empty with
    //                | true -> 
    //                    div [style "width:100%; margin: 10 0 10 10"] [text "Please select some annotations"]                                                  
    //                | false -> 
    //                    let text1 = 
    //                        selectedAnnos 
    //                        |> AMap.count 
    //                        |> AVal.map (fun n -> sprintf "%s annotation(s) selected" (n.ToString()))

    //                    div [style "width:100%; margin: 10 5 10 10"] [Incremental.text text1]

    //                    div [style "width:100%; margin: 10 5 10 10"] [
    //                        text "Please select a measurement to see statistics" 
    //                        mTypeDropdown
    //                    ]     
                        
    //                    Incremental.div (AttributeMap.ofList [style "width:100%; height:auto; margin: 0 5 0 5"]) 
    //                        (                                  
    //                            m.properties 
    //                            |> AMap.map(fun _ v -> 
    //                                div[] [
    //                                    button [clazz "ui button tiny inverted"; onClick (fun _ -> DeleteMeasurement v.measurementType )] [
    //                                     i [clazz "trash can icon"] []
    //                                     text "delete property"
    //                                        ]                        
    //                                    StatisticsMeasurement_App.view v|> UI.map (fun f -> MeasurementMessage (v.measurementType,f))
    //                                ]
    //                        ) 
    //                        |> AMap.toASet 
    //                        |> ASet.toAList 
    //                        |> AList.map(fun (a,b) -> b)                                                                 
    //                    )
    //                }
    //    )
            









    //Incremental.div (attributemap.oflist [style "width:100%; height:auto; margin: 0 5 0 5"]) 
    //        (                                  
    //            m.properties 
    //            |> amap.map(fun _ v -> 
    //                div[] [
    //                    button [clazz "ui button tiny inverted"; onclick (fun _ -> deletemeasurement v.measurementtype )] [
    //                            i [clazz "trash can icon"] []
    //                            text "delete property"
    //                        ]                        
    //                    statisticsmeasurement_app.view v|> ui.map (fun f -> measurementmessage (v.measurementtype,f))
    //                ]
    //            ) 
    //            |> amap.toaset 
    //            |> aset.toalist 
    //            |> alist.map(fun (a,b) -> b)                                                                 
    //        )




