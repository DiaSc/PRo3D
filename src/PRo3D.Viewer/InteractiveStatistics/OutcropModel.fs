namespace PRo3D.Viewer.InteractiveStatistics

open System
open FSharp.Data.Adaptive
open Adaptify
open PRo3D.Base.Annotation
open PRo3D.Core


[<ModelType>]
type OutcropModel = 
    {
        //note: Guid should be the same as the id of the Node
        aggregations : HashMap<Guid, InteractiveStatisticsModel>
        activeAggregation: Option<Guid> //currently used to determine if there is hovering going on in one InteractiveStatisticsModel        
        activeMeasurements: IndexList<Vis_Measurement>    
        allLeaves : HashMap<Guid, Guid> //the key Guid is the id of the Annotation, the value Guid is the id of the InteractiveStatisticsModel the Annotation belongs to
    }

type OutcropAction =
    | UpdateAllModels of InteractiveStatisticsAction //update all aggregations
    | InteractiveStatisticsMessage of Guid * InteractiveStatisticsAction //update a single aggregation
    | CreateAggregation of Node * GroupsModel 
    | RemoveAggregation of Guid


module OutcropModel =
    
    //let measurements = [LENGTH; DIP_AZIMUTH; STRIKE_AZIMUTH]    

    let initial =
        {
            aggregations = HashMap.empty
            activeAggregation = None            
            activeMeasurements = IndexList.Empty  
            allLeaves = HashMap.empty
        }
