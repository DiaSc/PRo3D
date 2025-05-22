//04045d79-4981-943d-b4ed-29a4c4198666
//309699a4-f3e4-fdd1-b21a-8a9197200590
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
type AdaptiveStatisticsMeasurementModel(value : StatisticsMeasurementModel) =
    let _data_ = FSharp.Data.Adaptive.cval(value.data)
    let _dataRange_ = FSharp.Data.Adaptive.cval(value.dataRange)
    let _avg_ = FSharp.Data.Adaptive.cval(value.avg)
    let _visualization_ = AdaptiveStatisticsVisualizationModel(value.visualization)
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : StatisticsMeasurementModel) = AdaptiveStatisticsMeasurementModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : StatisticsMeasurementModel) -> AdaptiveStatisticsMeasurementModel(value)) (fun (adaptive : AdaptiveStatisticsMeasurementModel) (value : StatisticsMeasurementModel) -> adaptive.Update(value))
    member __.Update(value : StatisticsMeasurementModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<StatisticsMeasurementModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _data_.Value <- value.data
            _dataRange_.Value <- value.dataRange
            _avg_.Value <- value.avg
            _visualization_.Update(value.visualization)
    member __.Current = __adaptive
    member __.measurementType = __value.measurementType
    member __.data = _data_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<(System.Guid * Microsoft.FSharp.Core.float)>>
    member __.dataRange = _dataRange_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.Range1d>
    member __.avg = _avg_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.visualization = _visualization_ :> FSharp.Data.Adaptive.aval<AdaptiveStatisticsVisualizationModelCase>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module StatisticsMeasurementModelLenses = 
    type StatisticsMeasurementModel with
        static member measurementType_ = ((fun (self : StatisticsMeasurementModel) -> self.measurementType), (fun (value : MeasurementType) (self : StatisticsMeasurementModel) -> { self with measurementType = value }))
        static member data_ = ((fun (self : StatisticsMeasurementModel) -> self.data), (fun (value : Microsoft.FSharp.Collections.List<(System.Guid * Microsoft.FSharp.Core.float)>) (self : StatisticsMeasurementModel) -> { self with data = value }))
        static member dataRange_ = ((fun (self : StatisticsMeasurementModel) -> self.dataRange), (fun (value : Aardvark.Base.Range1d) (self : StatisticsMeasurementModel) -> { self with dataRange = value }))
        static member avg_ = ((fun (self : StatisticsMeasurementModel) -> self.avg), (fun (value : Microsoft.FSharp.Core.float) (self : StatisticsMeasurementModel) -> { self with avg = value }))
        static member visualization_ = ((fun (self : StatisticsMeasurementModel) -> self.visualization), (fun (value : StatisticsVisualizationModel) (self : StatisticsMeasurementModel) -> { self with visualization = value }))

