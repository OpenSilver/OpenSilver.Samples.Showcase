namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.SampleRestWebService.Models

open System
open System.Collections.Generic
open System.IO
open System.Net
open System.Runtime.Serialization
open System.Text
open System.Windows
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("REST", "API", "HttpClient", "HTTP", "web", "service")>]
type REST_HttpClient_Demo() as this =
    inherit REST_HttpClient_DemoXaml()

    let mutable _ownerId : Guid = Guid.NewGuid()

    do
        this.InitializeComponent()

        // The "Owner ID" ensures that every person that uses the Showcase App has its own list of To-Do's:
        _ownerId <- Guid.NewGuid()

    member private this.RefreshRestToDos () =
        async {
            try
                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                use httpClient = new System.Net.Http.HttpClient()
                httpClient.DefaultRequestHeaders.Accept.Clear()
                httpClient.DefaultRequestHeaders.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/xml"))
                let! responseMessage = httpClient.GetAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo?OwnerId=" + _ownerId.ToString()) |> Async.AwaitTask
                let! response = responseMessage.Content.ReadAsStringAsync() |> Async.AwaitTask
                let serializer = DataContractSerializer(typeof<List<ToDoItem>>)
                use reader = new StringReader(response)
                use xmlReader = System.Xml.XmlReader.Create(reader)
                let toDoItems = serializer.ReadObject(xmlReader) :?> List<ToDoItem>
                this.RestToDosItemsControl.ItemsSource <- toDoItems
            with
            | ex -> MessageBox.Show("ERROR: " + ex.ToString()) |> ignore
        }

    member private this.ButtonRefreshRestToDos_Click(sender : obj, e : RoutedEventArgs) =
        async {
            let button = sender :?> Button
            button.Content <- "Please wait..."
            button.IsEnabled <- false

            do! this.RefreshRestToDos()

            button.IsEnabled <- true
            button.Content <- "Refresh the list"
        } |> Async.Start

    member private this.ButtonAddRestToDo_Click(sender : obj, e : RoutedEventArgs) =
        async {
            let button = sender :?> Button
            button.Content <- "Please wait..."
            button.IsEnabled <- false

            try
                let data = sprintf """{"OwnerId": "%s","Id": "%s","Description": "%s"}""" (_ownerId.ToString()) (Guid.NewGuid().ToString()) (this.RestToDoTextBox.Text.Replace("\"", "'"))

                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                use httpClient = new System.Net.Http.HttpClient()
                let! responseMessage = httpClient.PostAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/", new System.Net.Http.StringContent(data, Encoding.UTF8, "application/json")) |> Async.AwaitTask

                do! this.RefreshRestToDos()
            with
            | ex -> MessageBox.Show("ERROR: " + ex.ToString()) |> ignore

            button.IsEnabled <- true
            button.Content <- "Create"
        } |> Async.Start

    member private this.ButtonDeleteRestToDo_Click(sender : obj, e : RoutedEventArgs) =
        async {
            let button = sender :?> Button
            button.Content <- "Please wait..."
            button.IsEnabled <- false

            try
                let todo = button.DataContext :?> ToDoItem

                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                use httpClient = new System.Net.Http.HttpClient()
                let! responseMessage = httpClient.DeleteAsync(sprintf "https://cshtml5-rest-sample.azurewebsites.net/api/Todo/%s?OwnerId=%s" (todo.Id.ToString()) (_ownerId.ToString())) |> Async.AwaitTask

                do! this.RefreshRestToDos()
            with
            | ex -> MessageBox.Show("ERROR: " + ex.ToString()) |> ignore

            button.IsEnabled <- true
            button.Content <- "Delete"
        } |> Async.Start

    member private this.ButtonUpdateRestToDo_Click(sender : obj, e : RoutedEventArgs) =
        async {
            let button = sender :?> Button
            let todo = button.DataContext :?> ToDoItem

            // Verify that the new description of the To-Do is different from the previous description:
            let previousDescription = todo.Description
            let newDescription = this.RestToDoTextBox.Text.Replace("\"", "'")
            if newDescription = previousDescription then
                MessageBox.Show("To update the To-Do, please enter a different text in the field above, and then click the 'Update' button.") |> ignore
                return ()

            button.Content <- "Please wait..."
            button.IsEnabled <- false

            try
                let data = sprintf """{"OwnerId": "%s","Id": "%s","Description": "%s"}""" (_ownerId.ToString()) (todo.Id.ToString()) (this.RestToDoTextBox.Text.Replace("\"", "'"))

                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                use httpClient = new System.Net.Http.HttpClient()
                let! responseMessage = httpClient.PutAsync(sprintf "https://cshtml5-rest-sample.azurewebsites.net/api/Todo/%s" (todo.Id.ToString()), 
                    new System.Net.Http.StringContent(data, Encoding.UTF8, "application/json")) |> Async.AwaitTask

                do! this.RefreshRestToDos()
            with
            | ex -> MessageBox.Show("ERROR: " + ex.ToString()) |> ignore

            button.IsEnabled <- true
            button.Content <- "Update"
        } |> Async.Start
