namespace PRo3D.Viewer.InteractiveStatistics

open System
open PRo3D.Base.Annotation
open PRo3D.Core
open FSharp.Data.Adaptive
open Adaptify

type Vis_Measurement =
    | LENGTH
    | DIP_AZIMUTH
    | STRIKE_AZIMUTH

[<ModelType>]
type StatisticsVisualizationModel =         
    | Histogram of value: HistogramModel 
    | RoseDiagram of value: RoseDiagramModel
     with     
      member s.id =
          match s with          
          | Histogram    h -> h.id
          | RoseDiagram  r -> r.id         

      member s.hoveringActive = 
        match s with          
          | Histogram    h -> h.hoveredBin
          | RoseDiagram  r -> r.hoveredBin

      //member s.measurement : Vis_Measurement

type StatisticsVisualizationAction =
    | HistogramMessage of HistogramModelAction
    | RoseDiagramMessage of RoseDiagramModelAction    


module StatisticsVisualizationModel =

    let getLength = fun (x:AnnotationResults) -> x.length        
    let getDipAzimuth = fun (x:DipAndStrikeResults) -> x.dipAzimuth
    let getStrikeAzimuth = fun (x:DipAndStrikeResults) -> x.strikeAzimuth

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

    
    let getVisualizationData (annotations: List<Guid*Annotation>) (measurement:Vis_Measurement) =
        match measurement with
        | LENGTH -> getAnnotationResults annotations getLength         
        | DIP_AZIMUTH -> getDnSResults annotations getDipAzimuth            
        | STRIKE_AZIMUTH -> getDnSResults annotations getStrikeAzimuth

    let createVisualization (annotations: List<Guid*Annotation>) (measurement:Vis_Measurement) =
        let data = getVisualizationData annotations measurement
        match measurement with
        | LENGTH -> StatisticsVisualizationModel.Histogram (HistogramModel.initHistogram data)
        | DIP_AZIMUTH -> StatisticsVisualizationModel.RoseDiagram (RoseDiagramModel.initRoseDiagram data)
        | STRIKE_AZIMUTH -> StatisticsVisualizationModel.RoseDiagram (RoseDiagramModel.initRoseDiagram data)    

