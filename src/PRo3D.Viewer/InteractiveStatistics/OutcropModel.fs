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
    }

type OutcropAction =
    | InteractiveStatisticsMessage of InteractiveStatisticsAction
    | CreateAggregation of Node * GroupsModel    
    | RemoveAggregation of Guid


module OutcropModel =
    let initial =
        {
            aggregations = HashMap.empty            
        }
