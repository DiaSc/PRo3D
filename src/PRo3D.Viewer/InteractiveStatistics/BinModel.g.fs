//9937251f-75af-a1b2-8f57-c1acbcbcabc0
//94263772-9ab2-0e6b-873a-4677cefa3855
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
type AdaptiveBinModel(value : BinModel) =
    let _count_ = FSharp.Data.Adaptive.cval(value.count)
    let _range_ = FSharp.Data.Adaptive.cval(value.range)
    let _annotationIDs_ = FSharp.Data.Adaptive.cval(value.annotationIDs)
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : BinModel) = AdaptiveBinModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : BinModel) -> AdaptiveBinModel(value)) (fun (adaptive : AdaptiveBinModel) (value : BinModel) -> adaptive.Update(value))
    member __.Update(value : BinModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<BinModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _count_.Value <- value.count
            _range_.Value <- value.range
            _annotationIDs_.Value <- value.annotationIDs
    member __.Current = __adaptive
    member __.id = __value.id
    member __.count = _count_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.int>
    member __.range = _range_ :> FSharp.Data.Adaptive.aval<Aardvark.Base.Range1d>
    member __.annotationIDs = _annotationIDs_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.List<System.Guid>>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module BinModelLenses = 
    type BinModel with
        static member id_ = ((fun (self : BinModel) -> self.id), (fun (value : Microsoft.FSharp.Core.int) (self : BinModel) -> { self with id = value }))
        static member count_ = ((fun (self : BinModel) -> self.count), (fun (value : Microsoft.FSharp.Core.int) (self : BinModel) -> { self with count = value }))
        static member range_ = ((fun (self : BinModel) -> self.range), (fun (value : Aardvark.Base.Range1d) (self : BinModel) -> { self with range = value }))
        static member annotationIDs_ = ((fun (self : BinModel) -> self.annotationIDs), (fun (value : Microsoft.FSharp.Collections.List<System.Guid>) (self : BinModel) -> { self with annotationIDs = value }))

