//e29127c2-5199-9ded-e25b-fdf2653a2425
//e943559e-87b8-a030-ec0e-c4b0cd7fe6fc
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
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : OutcropModel) = AdaptiveOutcropModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : OutcropModel) -> AdaptiveOutcropModel(value)) (fun (adaptive : AdaptiveOutcropModel) (value : OutcropModel) -> adaptive.Update(value))
    member __.Update(value : OutcropModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<OutcropModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _aggregations_.Update(value.aggregations)
    member __.Current = __adaptive
    member __.aggregations = _aggregations_ :> FSharp.Data.Adaptive.amap<System.Guid, AdaptiveInteractiveStatisticsModel>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module OutcropModelLenses = 
    type OutcropModel with
        static member aggregations_ = ((fun (self : OutcropModel) -> self.aggregations), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, InteractiveStatisticsModel>) (self : OutcropModel) -> { self with aggregations = value }))

