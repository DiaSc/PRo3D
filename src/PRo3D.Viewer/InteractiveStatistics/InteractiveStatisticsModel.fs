namespace PRo3D.Viewer.InteractiveStatistics

open System
open FSharp.Data.Adaptive
open Adaptify
open PRo3D.Base.Annotation
open PRo3D.Core

[<ModelType>]
type InteractiveStatisticsModel = 
    {
        selectedAnnotations : HashMap<Guid, Annotation>
        properties          : HashMap<MeasurementType, StatisticsMeasurementModel> 
    }

type AnnoStatsAction =
    | UpdateSingleSelectedAnnotation of Guid * GroupsModel
    | UpdateMultipleSelectedAnnotations of GroupsModel
    | AddNewMeasurement of MeasurementType
    | MeasurementMessage of MeasurementType * StatisticsMeasurementAction
    | PredictStart of Annotation
    | PredictEnd    
    | DeleteMeasurement of MeasurementType
    | Reset //no more annotations are selected

module InteractiveStatisticsModel =
    let initial =
        {
            selectedAnnotations = HashMap.empty
            properties          = HashMap.empty
        }






