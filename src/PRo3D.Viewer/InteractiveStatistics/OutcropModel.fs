namespace PRo3D.Viewer.InteractiveStatistics

open System
open FSharp.Data.Adaptive
open Adaptify
open PRo3D.Base.Annotation
open PRo3D.Core

//TODO: add more 
type Measurement = 
    | LENGTH    
    | DIP_AZIMUTH 
    | STRIKE_AZIMUTH

[<ModelType>]
type OutcropModel = 
    {
        //note: Guid should be the same as the id of the Node
        aggregations : HashMap<Guid, InteractiveStatisticsModel>
        activeAggregation: Option<Guid> //currently used to determine if there is hovering going on in one InteractiveStatisticsModel
        availableMeasurements: List<Measurement>
        activeMeasurements: List<Measurement>
        defaultMeasurement: Measurement
    }

type OutcropAction =
    | InteractiveStatisticsMessage of Guid * InteractiveStatisticsAction
    | CreateAggregation of Node * GroupsModel    
    | RemoveAggregation of Guid


module OutcropModel =

    let defaultM = DIP_AZIMUTH
    let measurements = [LENGTH; DIP_AZIMUTH; STRIKE_AZIMUTH]
    let activeMeasurements = [defaultM]

    let initial =
        {
            aggregations = HashMap.empty
            activeAggregation = None
            availableMeasurements = measurements
            activeMeasurements = activeMeasurements
            defaultMeasurement = defaultM
        }
