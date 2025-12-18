//4ffc31bd-f209-d52b-8b46-81e618baf071
//29da4f1e-ac7b-d9b1-0d3a-2469c6a06142
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
type AdaptiveOutcropModel(value : OutcropModel) =
    let _aggregations_ =
        let inline __arg2 (m : AdaptiveInteractiveStatisticsModel) (v : InteractiveStatisticsModel) =
            m.Update(v)
            m
        FSharp.Data.Traceable.ChangeableModelMap(value.aggregations, (fun (v : InteractiveStatisticsModel) -> AdaptiveInteractiveStatisticsModel(v)), __arg2, (fun (m : AdaptiveInteractiveStatisticsModel) -> m))
    let _activeAggregation_ = FSharp.Data.Adaptive.cval(value.activeAggregation)
    let _availableMeasurements_ = FSharp.Data.Adaptive.cval(value.availableMeasurements)
    let _activeMeasurements_ = FSharp.Data.Adaptive.cval(value.activeMeasurements)
    let _defaultMeasurement_ = FSharp.Data.Adaptive.cval(value.defaultMeasurement)
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : OutcropModel) = AdaptiveOutcropModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : OutcropModel) -> AdaptiveOutcropModel(value)) (fun (adaptive : AdaptiveOutcropModel) (value : OutcropModel) -> adaptive.Update(value))
    member __.Update(value : OutcropModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<OutcropModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _aggregations_.Update(value.aggregations)
            _activeAggregation_.Value <- value.activeAggregation
            _availableMeasurements_.Value <- value.availableMeasurements
            _activeMeasurements_.Value <- value.activeMeasurements
            _defaultMeasurement_.Value <- value.defaultMeasurement
    member __.Current = __adaptive
    member __.aggregations = _aggregations_ :> FSharp.Data.Adaptive.amap<System.Guid, AdaptiveInteractiveStatisticsModel>
    member __.activeAggregation = _activeAggregation_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<System.Guid>>
    member __.availableMeasurements = _availableMeasurements_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<Measurement>>
    member __.activeMeasurements = _activeMeasurements_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<Measurement>>
    member __.defaultMeasurement = _defaultMeasurement_ :> FSharp.Data.Adaptive.aval<Measurement>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module OutcropModelLenses = 
    type OutcropModel with
        static member aggregations_ = ((fun (self : OutcropModel) -> self.aggregations), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, InteractiveStatisticsModel>) (self : OutcropModel) -> { self with aggregations = value }))
        static member activeAggregation_ = ((fun (self : OutcropModel) -> self.activeAggregation), (fun (value : Microsoft.FSharp.Core.Option<System.Guid>) (self : OutcropModel) -> { self with activeAggregation = value }))
        static member availableMeasurements_ = ((fun (self : OutcropModel) -> self.availableMeasurements), (fun (value : Microsoft.FSharp.Collections.List<Measurement>) (self : OutcropModel) -> { self with availableMeasurements = value }))
        static member activeMeasurements_ = ((fun (self : OutcropModel) -> self.activeMeasurements), (fun (value : Microsoft.FSharp.Collections.List<Measurement>) (self : OutcropModel) -> { self with activeMeasurements = value }))
        static member defaultMeasurement_ = ((fun (self : OutcropModel) -> self.defaultMeasurement), (fun (value : Measurement) (self : OutcropModel) -> { self with defaultMeasurement = value }))

