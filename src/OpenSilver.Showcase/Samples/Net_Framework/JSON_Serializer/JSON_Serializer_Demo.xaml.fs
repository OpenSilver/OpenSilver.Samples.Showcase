namespace OpenSilver.Showcase

open System
open System.Collections.Generic
open System.Text.Json
open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("JSON", "serialization", "deserialization", "serialize")>]
type JSON_Serializer_Demo() as this =
    inherit JSON_Serializer_DemoXaml()

    let mutable json = ""
    let product =
        {
            Name = "TestProduct"
            ProductType = ProductType.B2C
            Price = 12.5
            Count = 341
            IsAvailable = true
            Sizes = [| "Small"; "Medium"; "Large" |]
            Features = new List<Feature>()
            ReleaseDate = DateTime.Now
        }

    do
        this.InitializeComponent()

    member _.Button_Click_Serialization(sender: obj, e: RoutedEventArgs) =
        json <- JsonSerializer.Serialize(product, JsonSerializerOptions(WriteIndented = true))
        MessageBox.Show(json) |> ignore

    member _.Button_Click_StronglyTypedDeserialization(sender: obj, e: RoutedEventArgs) =
        if not (String.IsNullOrEmpty(json)) then
            let deserializedProduct = JsonSerializer.Deserialize<Product>(json)
            let msg = sprintf "Name of the second feature: %s\nName of the third available size: %s\nRelease date: %O"
                                deserializedProduct.Features.[1].Name
                                deserializedProduct.Sizes.[2]
                                deserializedProduct.ReleaseDate
            MessageBox.Show(msg) |> ignore
        else
            MessageBox.Show("Please click on the Serialize button first.") |> ignore

    member _.Button_Click_DynamicDeserialization(sender: obj, e: RoutedEventArgs) =
        if not (String.IsNullOrEmpty(json)) then
            let root = JsonDocument.Parse(json).RootElement
            let name = root.GetProperty("Name").GetString()
            let feature2 = root.GetProperty("Features").[1].GetProperty("Name").GetString()
            let size3 = root.GetProperty("Sizes").[2].GetString()
            let msg = sprintf "Product name: %s\nName of the second feature: %s\nName of the third available size: %s" name feature2 size3
            MessageBox.Show(msg) |> ignore
        else
            MessageBox.Show("Please click on the Serialize button first.") |> ignore

and Product = {
    Name: string
    ProductType: ProductType
    Price: float
    Count: int
    IsAvailable: bool
    Sizes: string[]
    Features: List<Feature>
    ReleaseDate: DateTime
}

and Feature = {
    Name: string
}

and ProductType =
    | B2B = 0
    | B2C = 1
