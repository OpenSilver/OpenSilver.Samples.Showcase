namespace OpenSilver.Samples.Showcase.SampleRestWebService.Models

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks

type ToDoItem(description : string, ownerId : int) =
    member val Id : Guid = Guid.NewGuid() with get, set
    member val OwnerId : Guid = Guid.NewGuid() with get, set
    member val Description : string = description with get, set
