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
    
    //let getAnnotationResults
    //    (annotations: List<Guid*Annotation>)  
    //    (annotationProperty: AnnotationResults -> float) 
    //    = 
    //    annotations 
    //    |> List.map(fun (annoId, annotation) ->         
    //        match annotation.results with
    //        | Some a -> Some(annoId, a |> annotationProperty)
    //        | None -> None
    //    )
    //    |> List.choose(fun o -> o) 

    //let getDnSResults 
    //    (annotations: List<Guid*Annotation>)   
    //    (dnsProperty: DipAndStrikeResults -> float) 
    //    =
    //    annotations 
    //    |> List.map(fun (annoId, annotation) ->         
    //        match annotation.dnsResults with
    //        | Some a -> Some(annoId, a |> dnsProperty)
    //        | None -> None
    //    )
    //    |> List.choose(fun o -> o)  

    //let getLength = fun (x:AnnotationResults) -> x.length
    //let getBearing = fun (x:AnnotationResults) -> x.bearing    
    //let getDipAzimuth = fun (x:DipAndStrikeResults) -> x.dipAzimuth
    //let getStrikeAzimuth = fun (x:DipAndStrikeResults) -> x.strikeAzimuth

    //let getMeasurementData (mType:MeasurementType) (selected:List<Guid*Annotation>) =
    //    match mType.kind with
    //    | Kind.LENGTH -> getAnnotationResults selected getLength     
    //    | Kind.BEARING -> getAnnotationResults selected getBearing            
    //    | Kind.DIP_AZIMUTH -> getDnSResults selected getDipAzimuth
    //    | Kind.STRIKE_AZIMUTH -> getDnSResults selected getStrikeAzimuth


    let update (m:InteractiveStatisticsModel) (a:InteractiveStatisticsAction) =
        match a with
        | AddAnnotation (id) -> m
        | RemoveAnnotation (id) -> m        
        | StatisticsVisualizationMessage (id,msg) ->   
            let updatedVisualizations = m.visualisations |> HashMap.alter id (
                fun o -> 
                    match o with
                    | Some model -> Some(StatisticsVisualization_App.update model msg)
                    | None -> None            
                )  
            {m with visualisations = updatedVisualizations}                
          
       
  
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


                
//TODO: just show all visualisations side by side (horizontally)

//UI related 
module AnnotationStatisticsDrawings =

    //let mTypeDropdown =        
    //    div [ clazz "ui menu"; style "width:150px; height:20px;padding:0px; margin:0px"] [
    //        onBoot "$('#__ID__').dropdown('on', 'hover');" (
    //            div [ clazz "ui dropdown item"; style "width:100%"] [
    //                text "Properties"
    //                i [clazz "dropdown icon"; style "margin:0px 5px"] [] 
    //                div [ clazz "ui menu"] [
    //                    div [clazz "ui inverted item"; onMouseClick (fun _ -> AddNewMeasurement (StatisticsMeasurementModel.initMeasurementType Kind.LENGTH Scale.Metric))] [text "Length"]
    //                    div [clazz "ui inverted item"; onMouseClick (fun _ -> AddNewMeasurement (StatisticsMeasurementModel.initMeasurementType Kind.BEARING Scale.Angular))] [text "Bearing"]
    //                    div [clazz "ui inverted item"; onMouseClick (fun _ -> AddNewMeasurement (StatisticsMeasurementModel.initMeasurementType Kind.DIP_AZIMUTH Scale.Angular))] [text "Dip Azimuth"] 
    //                    div [clazz "ui inverted item"; onMouseClick (fun _ -> AddNewMeasurement (StatisticsMeasurementModel.initMeasurementType Kind.STRIKE_AZIMUTH Scale.Angular))] [text "Strike Azimuth"] 
    //                ]
    //            ]
    //        )
    //    ] 

    let view (m:AdaptiveInteractiveStatisticsModel) =

        //TODO: just show all visualisations side by side (horizontally)                     
           
        let RDs =             
            Incremental.div AttributeMap.empty (
                m.visualisations 
                |> AMap.map (fun k v -> 
                    div[style "float:left"] [StatisticsVisualization_App.drawVisualization2 v (new V2i(300, 150)) |> UI.map (fun f -> StatisticsVisualizationMessage (k,f))]
                ) 
                |> AMap.toASet 
                |> ASet.toAList 
                |> AList.map(fun (a,b) -> b)
            )
 
        let description = AVal.map2 (fun x y -> sprintf "Aggregation for: %A | N: %A" x y) m.node.name (m.leaves |> AMap.count)

        div [style "position: absolute; top: 15px; left: 15px;"] [
            div [style "color: white; font-family:Consolas; font-size:16;"] [Incremental.text description]
            RDs
        ]
        

        
            
                



                     
      
  
     

        //let v = 
        //    div[][StatisticsVisualization_App.drawVisualization m.visualisations. (new V2i(300, 150)) |> UI.map StatisticsVisualizationMessage]
             
        
        
        








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




