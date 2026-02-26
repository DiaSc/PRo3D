//47efb5f0-9b01-fa03-d666-de3b3e43ef88
//41b46a66-1da1-6b78-2667-0a9e3b8ab6fb
#nowarn "49" // upper case patterns
#nowarn "66" // upcast is unncecessary
#nowarn "1337" // internal types
#nowarn "1182" // value is unused
namespace rec PRo3D.Viewer.InteractiveStatistics

open System
open FSharp.Data.Adaptive
open Adaptify
open PRo3D.Viewer.InteractiveStatistics
[<System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
type AdaptiveStatisticsVisualizationModelCase =
    abstract member Update : StatisticsVisualizationModel -> AdaptiveStatisticsVisualizationModelCase
[<System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
type private AdaptiveStatisticsVisualizationModelHistogram(value : HistogramModel) =
    let _value_ = AdaptiveHistogramModel(value)
    let mutable __value = value
    member __.Update(value : HistogramModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<HistogramModel>.ShallowEquals(value, __value))) then
            __value <- value
            _value_.Update(value)
    member __.value = _value_
    interface AdaptiveStatisticsVisualizationModelCase with
        member x.Update(value : StatisticsVisualizationModel) =
            match value with
            | StatisticsVisualizationModel.Histogram(value) ->
                x.Update(value)
                x :> AdaptiveStatisticsVisualizationModelCase
            | StatisticsVisualizationModel.RoseDiagram(value) -> AdaptiveStatisticsVisualizationModelRoseDiagram(value) :> AdaptiveStatisticsVisualizationModelCase
[<System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
type private AdaptiveStatisticsVisualizationModelRoseDiagram(value : RoseDiagramModel) =
    let _value_ = AdaptiveRoseDiagramModel(value)
    let mutable __value = value
    member __.Update(value : RoseDiagramModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<RoseDiagramModel>.ShallowEquals(value, __value))) then
            __value <- value
            _value_.Update(value)
    member __.value = _value_
    interface AdaptiveStatisticsVisualizationModelCase with
        member x.Update(value : StatisticsVisualizationModel) =
            match value with
            | StatisticsVisualizationModel.Histogram(value) -> AdaptiveStatisticsVisualizationModelHistogram(value) :> AdaptiveStatisticsVisualizationModelCase
            | StatisticsVisualizationModel.RoseDiagram(value) ->
                x.Update(value)
                x :> AdaptiveStatisticsVisualizationModelCase
[<System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
type AdaptiveStatisticsVisualizationModel(value : StatisticsVisualizationModel) =
    inherit Adaptify.AdaptiveValue<AdaptiveStatisticsVisualizationModelCase>()
    let mutable __value = value
    let mutable __current =
        match value with
        | StatisticsVisualizationModel.Histogram(value) -> AdaptiveStatisticsVisualizationModelHistogram(value) :> AdaptiveStatisticsVisualizationModelCase
        | StatisticsVisualizationModel.RoseDiagram(value) -> AdaptiveStatisticsVisualizationModelRoseDiagram(value) :> AdaptiveStatisticsVisualizationModelCase
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (t : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member CreateAdaptiveCase(value : StatisticsVisualizationModel) =
        match value with
        | StatisticsVisualizationModel.Histogram(value) -> AdaptiveStatisticsVisualizationModelHistogram(value) :> AdaptiveStatisticsVisualizationModelCase
        | StatisticsVisualizationModel.RoseDiagram(value) -> AdaptiveStatisticsVisualizationModelRoseDiagram(value) :> AdaptiveStatisticsVisualizationModelCase
    static member Create(value : StatisticsVisualizationModel) = AdaptiveStatisticsVisualizationModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : StatisticsVisualizationModel) -> AdaptiveStatisticsVisualizationModel(value)) (fun (adaptive : AdaptiveStatisticsVisualizationModel) (value : StatisticsVisualizationModel) -> adaptive.Update(value))
    member __.Current = __adaptive
    member __.Update(value : StatisticsVisualizationModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<StatisticsVisualizationModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            let __n = __current.Update(value)
            if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<AdaptiveStatisticsVisualizationModelCase>.ShallowEquals(__n, __current))) then
                __current <- __n
                __.MarkOutdated()
    override __.Compute(t : FSharp.Data.Adaptive.AdaptiveToken) = __current
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module AdaptiveStatisticsVisualizationModel = 
    let (|AdaptiveHistogram|AdaptiveRoseDiagram|) (value : AdaptiveStatisticsVisualizationModelCase) =
        match value with
        | (:? AdaptiveStatisticsVisualizationModelHistogram as histogram) -> AdaptiveHistogram(histogram.value)
        | (:? AdaptiveStatisticsVisualizationModelRoseDiagram as rosediagram) -> AdaptiveRoseDiagram(rosediagram.value)
        | _ -> failwith "unreachable"

