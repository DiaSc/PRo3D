//a78798f7-ae4f-7c58-1391-5082509a21da
//3d6c6e87-ab2c-7245-5ce8-40a66e0fec3f
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
type AdaptiveHistogramModel(value : HistogramModel) =
    let _data_ = FSharp.Data.Adaptive.cval(value.data)
    let _maxBinValue_ = FSharp.Data.Adaptive.cval(value.maxBinValue)
    let _numOfBins_ = Aardvark.UI.Primitives.AdaptiveNumericInput(value.numOfBins)
    let _domainStart_ = Aardvark.UI.Primitives.AdaptiveNumericInput(value.domainStart)
    let _domainEnd_ = Aardvark.UI.Primitives.AdaptiveNumericInput(value.domainEnd)
    let _bins_ = FSharp.Data.Adaptive.cval(value.bins)
    let _hoveredBin_ = FSharp.Data.Adaptive.cval(value.hoveredBin)
    let _peekItem_ = FSharp.Data.Adaptive.cval(value.peekItem)
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : HistogramModel) = AdaptiveHistogramModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : HistogramModel) -> AdaptiveHistogramModel(value)) (fun (adaptive : AdaptiveHistogramModel) (value : HistogramModel) -> adaptive.Update(value))
    member __.Update(value : HistogramModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<HistogramModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _data_.Value <- value.data
            _maxBinValue_.Value <- value.maxBinValue
            _numOfBins_.Update(value.numOfBins)
            _domainStart_.Update(value.domainStart)
            _domainEnd_.Update(value.domainEnd)
            _bins_.Value <- value.bins
            _hoveredBin_.Value <- value.hoveredBin
            _peekItem_.Value <- value.peekItem
    member __.Current = __adaptive
    member __.id = __value.id
    member __.data = _data_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<(System.Guid * Microsoft.FSharp.Core.float)>>
    member __.maxBinValue = _maxBinValue_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.int>
    member __.numOfBins = _numOfBins_
    member __.domainStart = _domainStart_
    member __.domainEnd = _domainEnd_
    member __.bins = _bins_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<BinModel>>
    member __.hoveredBin = _hoveredBin_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<Microsoft.FSharp.Core.int>>
    member __.peekItem = _peekItem_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<(Microsoft.FSharp.Core.int * Microsoft.FSharp.Core.float)>>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module HistogramModelLenses = 
    type HistogramModel with
        static member id_ = ((fun (self : HistogramModel) -> self.id), (fun (value : System.Guid) (self : HistogramModel) -> { self with id = value }))
        static member data_ = ((fun (self : HistogramModel) -> self.data), (fun (value : Microsoft.FSharp.Collections.List<(System.Guid * Microsoft.FSharp.Core.float)>) (self : HistogramModel) -> { self with data = value }))
        static member maxBinValue_ = ((fun (self : HistogramModel) -> self.maxBinValue), (fun (value : Microsoft.FSharp.Core.int) (self : HistogramModel) -> { self with maxBinValue = value }))
        static member numOfBins_ = ((fun (self : HistogramModel) -> self.numOfBins), (fun (value : Aardvark.UI.Primitives.NumericInput) (self : HistogramModel) -> { self with numOfBins = value }))
        static member domainStart_ = ((fun (self : HistogramModel) -> self.domainStart), (fun (value : Aardvark.UI.Primitives.NumericInput) (self : HistogramModel) -> { self with domainStart = value }))
        static member domainEnd_ = ((fun (self : HistogramModel) -> self.domainEnd), (fun (value : Aardvark.UI.Primitives.NumericInput) (self : HistogramModel) -> { self with domainEnd = value }))
        static member bins_ = ((fun (self : HistogramModel) -> self.bins), (fun (value : Microsoft.FSharp.Collections.List<BinModel>) (self : HistogramModel) -> { self with bins = value }))
        static member hoveredBin_ = ((fun (self : HistogramModel) -> self.hoveredBin), (fun (value : Microsoft.FSharp.Core.Option<Microsoft.FSharp.Core.int>) (self : HistogramModel) -> { self with hoveredBin = value }))
        static member peekItem_ = ((fun (self : HistogramModel) -> self.peekItem), (fun (value : Microsoft.FSharp.Core.Option<(Microsoft.FSharp.Core.int * Microsoft.FSharp.Core.float)>) (self : HistogramModel) -> { self with peekItem = value }))

