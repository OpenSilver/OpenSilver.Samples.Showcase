namespace OpenSilver.Samples.Showcase.SampleRestWebService.Models

open System.Runtime.Serialization

[<DataContract(Name = "ToDoItem", Namespace = "http://schemas.datacontract.org/2004/07/DotNetForHtml5.Showcase.SampleRestWebService.Models")>]
type ToDoItem = {
    [<field: DataMember(Name = "Description")>]
    Description: string
    [<field: DataMember(Name = "Id")>]
    Id: string
    [<field: DataMember(Name = "OwnerId")>]
    OwnerId: string
}
