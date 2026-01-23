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
        active          :   bool
        node            :   Node
        path            :   list<Index>
        leaves          :   HashMap<Guid,Annotation>
        hoveredLeaves   :   Option<list<Guid>>
        visualisations  :   HashMap<Vis_Measurement,StatisticsVisualizationModel>
    }

type InteractiveStatisticsAction =
    | SetActive
    | AddAnnotation of list<Guid * Annotation> //add one or multiple annotations
    | RemoveAnnotation of Guid
    | CreateVisualization of Vis_Measurement
    | StatisticsVisualizationMessage of Vis_Measurement * StatisticsVisualizationAction //visualisation settings have changed

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
        active = false
        node = initNode
        path = list.Empty
        leaves = HashMap.empty
        hoveredLeaves = None
        visualisations = HashMap.empty
        }

     //activeMeas : Measurements that are currently active (=selected by the user via dropdown)
     let createModel (node:Node) (data:list<Guid*Annotation>) (activeMeas : IndexList<Vis_Measurement>) =        

        let vis = activeMeas |> IndexList.toList |> List.map (fun elem -> (elem, StatisticsVisualizationModel.createVisualization data elem)) |> HashMap.ofList          

        {
            id = node.key
            active = true
            node = node
            path = list.Empty 
            leaves = (data |> HashMap.ofList)
            hoveredLeaves = None
            visualisations = vis
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









