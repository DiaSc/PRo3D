namespace PRo3D.Viewer.InteractiveStatistics

open System
open FSharp.Data.Adaptive
open Adaptify
open PRo3D.Base.Annotation
open PRo3D.Core

[<ModelType>]
type InteractiveStatisticsModel =
    {
        [<NonAdaptive>]
        id              :   Guid
        node            :   Node
        path            :   list<Index>
        leaves          :   HashMap<Guid,Annotation>
        hoveredLeaves   :   Option<list<Guid>>
        visualisations  :   HashMap<Guid,StatisticsVisualizationModel>
    }

type InteractiveStatisticsAction =
    | AddAnnotation of Guid
    | RemoveAnnotation of Guid
    | StatisticsVisualizationMessage of Guid * StatisticsVisualizationAction //visualisation settings have changed

module InteractiveStatisticsModel =

     let initNode = 
        {
        version  = -1
        name     = "node"
        key      = Guid.NewGuid()
        leaves   = IndexList.Empty
        subNodes = IndexList.Empty 
        visible  = true
        expanded = true
        }


     let initial =
        {
        id = Guid.NewGuid()
        node = initNode
        path = list.Empty
        leaves = HashMap.empty
        hoveredLeaves = None
        visualisations = HashMap.empty
        }

     let getAnnotationResults
        (annotations: List<Guid*Annotation>)  
        (annotationProperty: AnnotationResults -> float) 
        = 
        annotations 
        |> List.map(fun (annoId, annotation) ->         
            match annotation.results with
            | Some a -> Some(annoId, a |> annotationProperty)
            | None -> None
        )
        |> List.choose(fun o -> o) 

     let getDnSResults 
        (annotations: List<Guid*Annotation>)   
        (dnsProperty: DipAndStrikeResults -> float) 
        =
        annotations 
        |> List.map(fun (annoId, annotation) ->         
            match annotation.dnsResults with
            | Some a -> Some(annoId, a |> dnsProperty)
            | None -> None
        )
        |> List.choose(fun o -> o)  

     let getLength = fun (x:AnnotationResults) -> x.length
     let getBearing = fun (x:AnnotationResults) -> x.bearing    
     let getDipAzimuth = fun (x:DipAndStrikeResults) -> x.dipAzimuth
     let getStrikeAzimuth = fun (x:DipAndStrikeResults) -> x.strikeAzimuth

     let createModel (node:Node) (data:HashMap<Guid, Leaf>)  =
        let annotations = 
                let test = node.leaves |> IndexList.toList
                test |> List.map (fun id -> 
                    match (data |> HashMap.tryFind id) with
                    | Some leaf -> Some(id,Leaf.toAnnotation leaf)
                    | None -> None
                )
                |> List.choose (fun entry -> entry) 

            //TODO: expand
        let data = getDnSResults annotations getDipAzimuth
        let data2 = getDnSResults annotations getStrikeAzimuth
        let data3 = getAnnotationResults annotations getLength

            //TODO: expand
        let vis = StatisticsVisualizationModel.RoseDiagram (RoseDiagramModel.initRoseDiagram data "dip Azimuth")     
        let vis2 = StatisticsVisualizationModel.RoseDiagram (RoseDiagramModel.initRoseDiagram data2 "strike Azimuth")
        let vis3 = StatisticsVisualizationModel.Histogram (HistogramModel.initHistogram data3 "length")

        let a = annotations |> HashMap.ofList

        let viz = HashMap.Empty |> HashMap.add vis.id vis |> HashMap.add vis2.id vis2 |> HashMap.add vis3.id vis3       
        

        {
            id = node.key
            node = node
            path = list.Empty 
            leaves = a 
            hoveredLeaves = None
            visualisations = viz
        }




//[<ModelType>]
//type InteractiveStatisticsModel = 
//    {
//        selectedAnnotations : HashMap<Guid, Annotation>
//        properties          : HashMap<MeasurementType, StatisticsMeasurementModel> 
//    }

//type AnnoStatsAction =
//    | UpdateSingleSelectedAnnotation of Guid * GroupsModel
//    | UpdateMultipleSelectedAnnotations of GroupsModel
//    | AddNewMeasurement of MeasurementType
//    | MeasurementMessage of MeasurementType * StatisticsMeasurementAction
//    | PredictStart of Annotation
//    | PredictEnd    
//    | DeleteMeasurement of MeasurementType
//    | Reset //no more annotations are selected
//    | CreateRDFromGroup of Node * GroupsModel

//module InteractiveStatisticsModel =
//    let initial =
//        {
//            selectedAnnotations = HashMap.empty
//            properties          = HashMap.empty
//        }









