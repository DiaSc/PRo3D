//75ad3186-d935-27bf-c744-d566bcf53351
//3a7cd48b-c284-3d4f-5d94-99f4cfdef2b1
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
type AdaptiveInteractiveStatisticsModel(value : InteractiveStatisticsModel) =
    let _selectedAnnotations_ =
        let inline __arg2 (m : PRo3D.Base.Annotation.AdaptiveAnnotation) (v : PRo3D.Base.Annotation.Annotation) =
            m.Update(v)
            m
        FSharp.Data.Traceable.ChangeableModelMap(value.selectedAnnotations, (fun (v : PRo3D.Base.Annotation.Annotation) -> PRo3D.Base.Annotation.AdaptiveAnnotation(v)), __arg2, (fun (m : PRo3D.Base.Annotation.AdaptiveAnnotation) -> m))
    let _properties_ =
        let inline __arg2 (m : AdaptiveStatisticsMeasurementModel) (v : StatisticsMeasurementModel) =
            m.Update(v)
            m
        FSharp.Data.Traceable.ChangeableModelMap(value.properties, (fun (v : StatisticsMeasurementModel) -> AdaptiveStatisticsMeasurementModel(v)), __arg2, (fun (m : AdaptiveStatisticsMeasurementModel) -> m))
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : InteractiveStatisticsModel) = AdaptiveInteractiveStatisticsModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : InteractiveStatisticsModel) -> AdaptiveInteractiveStatisticsModel(value)) (fun (adaptive : AdaptiveInteractiveStatisticsModel) (value : InteractiveStatisticsModel) -> adaptive.Update(value))
    member __.Update(value : InteractiveStatisticsModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<InteractiveStatisticsModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _selectedAnnotations_.Update(value.selectedAnnotations)
            _properties_.Update(value.properties)
    member __.Current = __adaptive
    member __.selectedAnnotations = _selectedAnnotations_ :> FSharp.Data.Adaptive.amap<System.Guid, PRo3D.Base.Annotation.AdaptiveAnnotation>
    member __.properties = _properties_ :> FSharp.Data.Adaptive.amap<MeasurementType, AdaptiveStatisticsMeasurementModel>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module InteractiveStatisticsModelLenses = 
    type InteractiveStatisticsModel with
        static member selectedAnnotations_ = ((fun (self : InteractiveStatisticsModel) -> self.selectedAnnotations), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, PRo3D.Base.Annotation.Annotation>) (self : InteractiveStatisticsModel) -> { self with selectedAnnotations = value }))
        static member properties_ = ((fun (self : InteractiveStatisticsModel) -> self.properties), (fun (value : FSharp.Data.Adaptive.HashMap<MeasurementType, StatisticsMeasurementModel>) (self : InteractiveStatisticsModel) -> { self with properties = value }))

