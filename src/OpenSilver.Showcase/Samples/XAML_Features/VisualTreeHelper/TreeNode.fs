namespace OpenSilver.Showcase

open System.Collections.Generic

type TreeNode(name: string) =
    new() = TreeNode(null)
    member val Name: string = name with get, set
    member val Children: List<TreeNode> = List<TreeNode>() with get, set