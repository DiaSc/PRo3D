namespace PRo3D.Viewer.InteractiveStatistics

open FSharp.Data.Adaptive
open Adaptify

[<ModelType>]
type StatisticsVisualizationModel = 
    | Histogram of value: HistogramModel 
    | RoseDiagram of value: RoseDiagramModel

type StatisticsVisualizationAction = 
    | HistogramMessage of HistogramModelAction
    | RoseDiagramMessage of RoseDiagramModelAction


//module StatisticsVisualizationModel

