//f7c760b3-3e2d-01cc-065e-4d65159892c9
//f876e966-ffef-3869-4e5b-363b80c4caf2
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
    let _active_ = FSharp.Data.Adaptive.cval(value.active)
    let _node_ = PRo3D.Core.AdaptiveNode(value.node)
    let _path_ = FSharp.Data.Adaptive.cval(value.path)
    let _leaves_ =
        let inline __arg2 (m : PRo3D.Base.Annotation.AdaptiveAnnotation) (v : PRo3D.Base.Annotation.Annotation) =
            m.Update(v)
            m
        FSharp.Data.Traceable.ChangeableModelMap(value.leaves, (fun (v : PRo3D.Base.Annotation.Annotation) -> PRo3D.Base.Annotation.AdaptiveAnnotation(v)), __arg2, (fun (m : PRo3D.Base.Annotation.AdaptiveAnnotation) -> m))
    let _hoveredLeaves_ = FSharp.Data.Adaptive.cval(value.hoveredLeaves)
    let _visualisations_ = FSharp.Data.Traceable.ChangeableModelMap(value.visualisations, (fun (v : StatisticsVisualizationModel) -> AdaptiveStatisticsVisualizationModel.CreateAdaptiveCase(v)), (fun (m : AdaptiveStatisticsVisualizationModelCase) (v : StatisticsVisualizationModel) -> m.Update(v)), (fun (m : AdaptiveStatisticsVisualizationModelCase) -> m))
    let mutable __value = value
    let __adaptive = FSharp.Data.Adaptive.AVal.custom((fun (token : FSharp.Data.Adaptive.AdaptiveToken) -> __value))
    static member Create(value : InteractiveStatisticsModel) = AdaptiveInteractiveStatisticsModel(value)
    static member Unpersist = Adaptify.Unpersist.create (fun (value : InteractiveStatisticsModel) -> AdaptiveInteractiveStatisticsModel(value)) (fun (adaptive : AdaptiveInteractiveStatisticsModel) (value : InteractiveStatisticsModel) -> adaptive.Update(value))
    member __.Update(value : InteractiveStatisticsModel) =
        if Microsoft.FSharp.Core.Operators.not((FSharp.Data.Adaptive.ShallowEqualityComparer<InteractiveStatisticsModel>.ShallowEquals(value, __value))) then
            __value <- value
            __adaptive.MarkOutdated()
            _active_.Value <- value.active
            _node_.Update(value.node)
            _path_.Value <- value.path
            _leaves_.Update(value.leaves)
            _hoveredLeaves_.Value <- value.hoveredLeaves
            _visualisations_.Update(value.visualisations)
    member __.Current = __adaptive
    member __.id = __value.id
    member __.active = _active_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.bool>
    member __.node = _node_
    member __.path = _path_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Collections.list<FSharp.Data.Adaptive.Index>>
    member __.leaves = _leaves_ :> FSharp.Data.Adaptive.amap<System.Guid, PRo3D.Base.Annotation.AdaptiveAnnotation>
    member __.hoveredLeaves = _hoveredLeaves_ :> FSharp.Data.Adaptive.aval<Microsoft.FSharp.Core.Option<Microsoft.FSharp.Collections.list<System.Guid>>>
    member __.visualisations = _visualisations_ :> FSharp.Data.Adaptive.amap<Vis_Measurement, AdaptiveStatisticsVisualizationModelCase>
[<AutoOpen; System.Diagnostics.CodeAnalysis.SuppressMessage("NameConventions", "*")>]
module InteractiveStatisticsModelLenses = 
    type InteractiveStatisticsModel with
        static member id_ = ((fun (self : InteractiveStatisticsModel) -> self.id), (fun (value : System.Guid) (self : InteractiveStatisticsModel) -> { self with id = value }))
        static member active_ = ((fun (self : InteractiveStatisticsModel) -> self.active), (fun (value : Microsoft.FSharp.Core.bool) (self : InteractiveStatisticsModel) -> { self with active = value }))
        static member node_ = ((fun (self : InteractiveStatisticsModel) -> self.node), (fun (value : PRo3D.Core.Node) (self : InteractiveStatisticsModel) -> { self with node = value }))
        static member path_ = ((fun (self : InteractiveStatisticsModel) -> self.path), (fun (value : Microsoft.FSharp.Collections.list<FSharp.Data.Adaptive.Index>) (self : InteractiveStatisticsModel) -> { self with path = value }))
        static member leaves_ = ((fun (self : InteractiveStatisticsModel) -> self.leaves), (fun (value : FSharp.Data.Adaptive.HashMap<System.Guid, PRo3D.Base.Annotation.Annotation>) (self : InteractiveStatisticsModel) -> { self with leaves = value }))
        static member hoveredLeaves_ = ((fun (self : InteractiveStatisticsModel) -> self.hoveredLeaves), (fun (value : Microsoft.FSharp.Core.Option<Microsoft.FSharp.Collections.list<System.Guid>>) (self : InteractiveStatisticsModel) -> { self with hoveredLeaves = value }))
        static member visualisations_ = ((fun (self : InteractiveStatisticsModel) -> self.visualisations), (fun (value : FSharp.Data.Adaptive.HashMap<Vis_Measurement, StatisticsVisualizationModel>) (self : InteractiveStatisticsModel) -> { self with visualisations = value }))

