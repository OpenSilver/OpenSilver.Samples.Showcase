namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks

type ViewSourceButtonInfo() =
    private new(relativePath: string, fileName: string) as this =
        ViewSourceButtonInfo()
        then
            this.RelativePath <- relativePath
            this.FileName <- fileName

    member val FilePathOnGitHub = "" with get, set
    member val FileName = "" with get, set
    member val RelativePath = "" with get, set
    member val Branch = "develop"  with get, set
    member val Repository = "OpenSilver.Samples.Showcase" with get, set
    member val Owner = "OpenSilver" with get, set
    member val TabHeader = "" with get, set
    
    member public this.GetHeader() =
        if not (String.IsNullOrEmpty(this.TabHeader)) then
            this.TabHeader
        else
            this.FileName

    member public this.GetAbsoluteUrl() =
        $"https://github.com/{this.Owner}/{this.Repository}/blob/{this.Branch}/{this.RelativePath}/{this.FileName}"