//83e77ee7-0758-e4ea-bb1b-09118561532a
//31b486f4-b188-0516-ab4e-373cf887b149
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
type AdaptiveRoseDiagramModel(value : RoseDiagramModel) =
    let _data_ = FSharp.Data.Adaptive.cval(value.data)
    let _maxBinValue_ = FSharp.Data.Adaptive.cval(value.maxBinValue)
    let _avgAngle_ = FSharp.Data.Adaptive.cval(value.avgAngle)
    let _bins_ = FSharp.Data.Adaptive.cval(value.bins)
    let _center_ = FSharp.Data.Adaptive.cval(value.center)
    let _innerRad_ = FSharp.Data.Adaptive.cval(value.innerRad)
    let _outerRad_ = FSharp.Data.Adaptive.cval(value.outerRad)
    let _binAngle_ = FSharp.Data.Adaptive.cval(value.binAngle)
    let _hoveredBin_ = FSharp.Data.Adaptive.cval(value.hoveredBin)
    let _peekItem_ = FSharp.Data.Adaptive.cval(value.peekItem)
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : RoseDiagramModel) = AdaptiveRoseDiagramModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : RoseDiagramModel) -> AdaptiveRoseDiagramModel(value)) (fun (adaptive : AdaptiveRoseDiagramModel) (value : RoseDiagramModel) -> adaptive.Update(value))
    member __.Update(value : RoseDiagramModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<RoseDiagramModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _data_.Value <- value.data
            _maxBinValue_.Value <- value.maxBinValue
            _avgAngle_.Value <- value.avgAngle
            _bins_.Value <- value.bins
            _center_.Value <- value.center
            _innerRad_.Value <- value.innerRad
            _outerRad_.Value <- value.outerRad
            _binAngle_.Value <- value.binAngle
            _hoveredBin_.Value <- value.hoveredBin
            _peekItem_.Value <- value.peekItem
    member __.Current = __adaptive
    member __.id = __value.id
    member __.value = __value.value
    member __.data = _data_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<(System.Guid * Microsoft.FSharp.Core.float)>>
    member __.maxBinValue = _maxBinValue_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.int>
    member __.avgAngle = _avgAngle_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.bins = _bins_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<BinModel>>
    member __.center = _center_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.V2d>
    member __.innerRad = _innerRad_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.outerRad = _outerRad_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.binAngle = _binAngle_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.float>
    member __.hoveredBin = _hoveredBin_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<Microsoft.FSharp.Core.int>>
    member __.peekItem = _peekItem_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<(Microsoft.FSharp.Core.int * Microsoft.FSharp.Core.float)>>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module RoseDiagramModelLenses = 
    type RoseDiagramModel with
        static member id_ = ((fun (self : RoseDiagramModel) -> self.id), (fun (value : System.Guid) (self : RoseDiagramModel) -> { self with id = value }))
        static member value_ = ((fun (self : RoseDiagramModel) -> self.value), (fun (value : Microsoft.FSharp.Core.string) (self : RoseDiagramModel) -> { self with value = value }))
        static member data_ = ((fun (self : RoseDiagramModel) -> self.data), (fun (value : Microsoft.FSharp.Collections.List<(System.Guid * Microsoft.FSharp.Core.float)>) (self : RoseDiagramModel) -> { self with data = value }))
        static member maxBinValue_ = ((fun (self : RoseDiagramModel) -> self.maxBinValue), (fun (value : Microsoft.FSharp.Core.int) (self : RoseDiagramModel) -> { self with maxBinValue = value }))
        static member avgAngle_ = ((fun (self : RoseDiagramModel) -> self.avgAngle), (fun (value : Microsoft.FSharp.Core.float) (self : RoseDiagramModel) -> { self with avgAngle = value }))
        static member bins_ = ((fun (self : RoseDiagramModel) -> self.bins), (fun (value : Microsoft.FSharp.Collections.List<BinModel>) (self : RoseDiagramModel) -> { self with bins = value }))
        static member center_ = ((fun (self : RoseDiagramModel) -> self.center), (fun (value : Aardvark.Base.V2d) (self : RoseDiagramModel) -> { self with center = value }))
        static member innerRad_ = ((fun (self : RoseDiagramModel) -> self.innerRad), (fun (value : Microsoft.FSharp.Core.float) (self : RoseDiagramModel) -> { self with innerRad = value }))
        static member outerRad_ = ((fun (self : RoseDiagramModel) -> self.outerRad), (fun (value : Microsoft.FSharp.Core.float) (self : RoseDiagramModel) -> { self with outerRad = value }))
        static member binAngle_ = ((fun (self : RoseDiagramModel) -> self.binAngle), (fun (value : Microsoft.FSharp.Core.float) (self : RoseDiagramModel) -> { self with binAngle = value }))
        static member hoveredBin_ = ((fun (self : RoseDiagramModel) -> self.hoveredBin), (fun (value : Microsoft.FSharp.Core.Option<Microsoft.FSharp.Core.int>) (self : RoseDiagramModel) -> { self with hoveredBin = value }))
        static member peekItem_ = ((fun (self : RoseDiagramModel) -> self.peekItem), (fun (value : Microsoft.FSharp.Core.Option<(Microsoft.FSharp.Core.int * Microsoft.FSharp.Core.float)>) (self : RoseDiagramModel) -> { self with peekItem = value }))

