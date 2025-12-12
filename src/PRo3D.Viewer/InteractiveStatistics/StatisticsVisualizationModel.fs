namespace PRo3D.Viewer.InteractiveStatistics

open FSharp.Data.Adaptive
open Adaptify

[<ModelType>]
type StatisticsVisualizationModel =         
    | Histogram of value: HistogramModel 
    | RoseDiagram of value: RoseDiagramModel
     with     
      member s.id =
          match s with          
          | Histogram    h -> h.id
          | RoseDiagram  r -> r.id

type StatisticsVisualizationAction = 
    | HistogramMessage of HistogramModelAction
    | RoseDiagramMessage of RoseDiagramModelAction


//module StatisticsVisualizationModel

