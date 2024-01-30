namespace OpenSilver.Samples.Showcase

open Newtonsoft.Json
open System
open System.Windows
open System.Windows.Controls

type Product() =
    member val Name = "" with get, set
    //member val ProductType = ProductType.B2C with get, set
    member val Price = 0.0 with get, set
    member val Count = 0 with get, set
    member val IsAvailable = false with get, set
    //member val Sizes = [||] with get, set
    //member val Features = [] : Feature list with get, set
    member val ReleaseDate = DateTime.Now with get, set

and Feature() =
    member val Name = "" with get, set

and ProductType =
    | B2B
    | B2C

type JSON_Serializer_Demo() as this =
    inherit JSON_Serializer_DemoXaml()

    let mutable _json : string = ""
    let mutable _product : Product = Product(
            Name = "TestProduct",
            //ProductType = ProductType.B2C,
            Price = 12.50,
            Count = 341,
            IsAvailable = true,
            //Sizes = [|"Small"; "Medium"; "Large"|],
            //Features = [ Feature(Name = "TestFeature1"); Feature(Name = "TestFeature2"); Feature(Name = "TestFeature3") ],
            ReleaseDate = DateTime.Now
        )

    do
        this.InitializeComponent()

    member private this.Button_Click_Serialization(sender : obj , e : RoutedEventArgs) =
        // Serialize:
        _json <- JsonConvert.SerializeObject(_product, false)

        // Indent:
        let indentedJson = _json.Replace(",", ",\n")

        // Display the result:
        MessageBox.Show(indentedJson) |> ignore

        // Expected Result:
        //{  
        //   "Name":"TestProduct",
        //   "ProductType":"B2C",
        //   "Price":12.5,
        //   "Count":341,
        //   "IsAvailable":true,
        //   "Sizes":[  
        //      "Small",
        //      "Medium",
        //      "Large"
        //   ],
        //   "Features":[  
        //      {  
        //         "Name":"TestFeature1"
        //      },
        //      {  
        //         "Name":"TestFeature2"
        //      },
        //      {  
        //         "Name":"TestFeature3"
        //      }
        //   ],
        //   "ReleaseDate":"2017-04-10T16:26:41.754Z"
        //}

    member private this.Button_Click_StronglyTypedDeserialization (sender : obj, e : RoutedEventArgs) =
        async {
            if not (String.IsNullOrEmpty _json) then
                let! deserializedProduct = JsonConvert.DeserializeObject<Product>(_json, false) |> Async.AwaitTask

                MessageBox.Show("Product name: " + deserializedProduct.Name) |> ignore
                //MessageBox.Show("Name of the second feature: " + deserializedProduct.Features[1].Name) |> ignore
                //MessageBox.Show("Name of the third available size: " + deserializedProduct.Sizes[2]) |> ignore
                //MessageBox.Show("Release date: " + deserializedProduct.ReleaseDate.ToString()) |> ignore

                // Expected Result: "Name of the second feature: TestFeature2"
                // Expected Result: "Name of the third available size: Large"
            else
                MessageBox.Show("Please click on the Serialize button first.") |> ignore
        } |> Async.Start

    member private this.Button_Click_DynamicDeserialization(sender : obj, e : RoutedEventArgs) =
        async {
            if not (String.IsNullOrEmpty _json) then
                let! deserializedObject = JsonConvert.DeserializeObject<IJsonType>(_json, false) |> Async.AwaitTask

                MessageBox.Show("Product name: " + deserializedObject.["Name"].Value.ToString()) |> ignore
                //MessageBox.Show("Name of the second feature: " + deserializedObject.["Features"].[1].["Name"].Value.ToString()) |> ignore
                //MessageBox.Show("Name of the third available size: " + deserializedObject.["Sizes"].[2].Value.ToString()) |> ignore

                // Expected Result: "Product name: TestProduct"
                // Expected Result: "Name of the second feature: TestFeature2"
                // Expected Result: "Name of the third available size: Large"
            else
                MessageBox.Show("Please click on the Serialize button first.") |> ignore
        } |> Async.Start
    