//ee6d8025-97ac-8ffd-3aff-cdf94662535c
//fdd13480-849f-ecb0-e9df-ed00546b4aa8
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
    let _activePeeking_ = FSharp.Data.Adaptive.cval(value.activePeeking)
    let _activeMeasurements_ = FSharp.Data.Adaptive.clist(value.activeMeasurements)
    let _allLeaves_ = FSharp.Data.Adaptive.cmap(value.allLeaves)
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
            _activePeeking_.Value <- value.activePeeking
            _activeMeasurements_.Value <- value.activeMeasurements
            _allLeaves_.Value <- value.allLeaves
    member __.Current = __adaptive
    member __.aggregations = _aggregations_ :> FSharp.Data.Adaptive.amap<System.Guid, AdaptiveInteractiveStatisticsModel>
    member __.activeAggregation = _activeAggregation_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<System.Guid>>
    member __.activePeeking = _activePeeking_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<System.Guid>>
    member __.activeMeasurements = _activeMeasurements_ :> FSharp.Data.Adaptive.alist<Vis_Measurement>
    member __.allLeaves = _allLeaves_ :> FSharp.Data.Adaptive.amap<System.Guid, System.Guid>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module OutcropModelLenses = 
    type OutcropModel with
        static member aggregations_ = ((fun (self : OutcropModel) -> self.aggregations), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, InteractiveStatisticsModel>) (self : OutcropModel) -> { self with aggregations = value }))
        static member activeAggregation_ = ((fun (self : OutcropModel) -> self.activeAggregation), (fun (value : Microsoft.FSharp.Core.Option<System.Guid>) (self : OutcropModel) -> { self with activeAggregation = value }))
        static member activePeeking_ = ((fun (self : OutcropModel) -> self.activePeeking), (fun (value : Microsoft.FSharp.Core.Option<System.Guid>) (self : OutcropModel) -> { self with activePeeking = value }))
        static member activeMeasurements_ = ((fun (self : OutcropModel) -> self.activeMeasurements), (fun (value : FSharp.Data.Adaptive.IndexList<Vis_Measurement>) (self : OutcropModel) -> { self with activeMeasurements = value }))
        static member allLeaves_ = ((fun (self : OutcropModel) -> self.allLeaves), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, System.Guid>) (self : OutcropModel) -> { self with allLeaves = value }))

