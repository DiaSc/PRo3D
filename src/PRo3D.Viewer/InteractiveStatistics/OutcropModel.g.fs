//fe782230-a9b1-8bb3-d1ae-f0eac57134c2
//a1c1e046-5bbf-dce1-475b-b4a3cbae0583
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
    let _ranges_ = FSharp.Data.Adaptive.cmap(value.ranges)
    let _allLeaves_ = FSharp.Data.Adaptive.cmap(value.allLeaves)
    let _flat_ =
        let inline __arg2 (m : PRo3D.Base.Annotation.AdaptiveAnnotation) (v : PRo3D.Base.Annotation.Annotation) =
            m.Update(v)
            m
        FSharp.Data.Traceable.ChangeableModelMap(value.flat, (fun (v : PRo3D.Base.Annotation.Annotation) -> PRo3D.Base.Annotation.AdaptiveAnnotation(v)), __arg2, (fun (m : PRo3D.Base.Annotation.AdaptiveAnnotation) -> m))
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
            _ranges_.Value <- value.ranges
            _allLeaves_.Value <- value.allLeaves
            _flat_.Update(value.flat)
    member __.Current = __adaptive
    member __.aggregations = _aggregations_ :> FSharp.Data.Adaptive.amap<System.Guid, AdaptiveInteractiveStatisticsModel>
    member __.activeAggregation = _activeAggregation_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<System.Guid>>
    member __.activePeeking = _activePeeking_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<System.Guid>>
    member __.activeMeasurements = _activeMeasurements_ :> FSharp.Data.Adaptive.alist<Vis_Measurement>
    member __.ranges = _ranges_ :> FSharp.Data.Adaptive.amap<Vis_Measurement, Aardvark.Base.Range1d>
    member __.allLeaves = _allLeaves_ :> FSharp.Data.Adaptive.amap<System.Guid, System.Guid>
    member __.flat = _flat_ :> FSharp.Data.Adaptive.amap<System.Guid, PRo3D.Base.Annotation.AdaptiveAnnotation>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module OutcropModelLenses = 
    type OutcropModel with
        static member aggregations_ = ((fun (self : OutcropModel) -> self.aggregations), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, InteractiveStatisticsModel>) (self : OutcropModel) -> { self with aggregations = value }))
        static member activeAggregation_ = ((fun (self : OutcropModel) -> self.activeAggregation), (fun (value : Microsoft.FSharp.Core.Option<System.Guid>) (self : OutcropModel) -> { self with activeAggregation = value }))
        static member activePeeking_ = ((fun (self : OutcropModel) -> self.activePeeking), (fun (value : Microsoft.FSharp.Core.Option<System.Guid>) (self : OutcropModel) -> { self with activePeeking = value }))
        static member activeMeasurements_ = ((fun (self : OutcropModel) -> self.activeMeasurements), (fun (value : FSharp.Data.Adaptive.IndexList<Vis_Measurement>) (self : OutcropModel) -> { self with activeMeasurements = value }))
        static member ranges_ = ((fun (self : OutcropModel) -> self.ranges), (fun (value : FSharp.Data.Adaptive.HashMap<Vis_Measurement, Aardvark.Base.Range1d>) (self : OutcropModel) -> { self with ranges = value }))
        static member allLeaves_ = ((fun (self : OutcropModel) -> self.allLeaves), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, System.Guid>) (self : OutcropModel) -> { self with allLeaves = value }))
        static member flat_ = ((fun (self : OutcropModel) -> self.flat), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, PRo3D.Base.Annotation.Annotation>) (self : OutcropModel) -> { self with flat = value }))

