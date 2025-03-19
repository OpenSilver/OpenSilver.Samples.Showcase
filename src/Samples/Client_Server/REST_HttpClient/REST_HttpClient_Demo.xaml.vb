Imports DotNetForHtml5.Showcase.SampleRestWebService.Models
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class REST_HttpClient_Demo
        Inherits UserControl
        Private _ownerId As Guid

        Public Sub New()
            Me.InitializeComponent()

            ' The "Owner ID" ensures that every person that uses the Showcase App has its own list of To-Do's:
            _ownerId = Guid.NewGuid()
        End Sub

        Private Async Function RefreshRestToDos() As Task
            Try
#If Not OPENSILVER Then
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers[HttpRequestHeader.Accept] = "application/xml";
                string response = await webClient.DownloadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo?OwnerId=" + _ownerId.ToString());
                var dataContractSerializer = new DataContractSerializer(typeof(List<ToDoItem>));
                List<ToDoItem> toDoItems = (List<ToDoItem>)dataContractSerializer.DeserializeFromString(response);
                RestToDosItemsControl.ItemsSource = toDoItems;
#Else
                'Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
                Dim httpClient = New Http.HttpClient()
                httpClient.DefaultRequestHeaders.Accept.Clear()
                httpClient.DefaultRequestHeaders.Accept.Add(Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/xml"))
                Dim responseMessage = Await httpClient.GetAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo?OwnerId=" & _ownerId.ToString())

                Dim response As String = Await responseMessage.Content.ReadAsStringAsync()

                Dim dataContractSerializer = New DataContractSerializer(GetType(List(Of ToDoItem)), New Type() {GetType(ToDoItem)})
                'convert the string into a stream so it can be deserialized:
                Using stream = New MemoryStream()
                    Using writer = New StreamWriter(stream)
                        writer.Write(response)
                        writer.Flush()
                        stream.Position = 0
                        Dim toDoItems = CType(dataContractSerializer.ReadObject(stream), List(Of ToDoItem))
                        Me.RestToDosItemsControl.ItemsSource = toDoItems
                    End Using
#End If
                End Using
            Catch ex As Exception
                Call MessageBox.Show("ERROR: " & ex.ToString())
            End Try
        End Function

        Private Async Sub ButtonRefreshRestToDos_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.Content = "Please wait..."
            button.IsEnabled = False

            Await RefreshRestToDos()

            button.IsEnabled = True
            button.Content = "Refresh the list"
        End Sub

        Private Async Sub ButtonAddRestToDo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.Content = "Please wait..."
            button.IsEnabled = False

            Try
                Dim data As String = String.Format("{{""OwnerId"": ""{0}"",""Id"": ""{1}"",""Description"": ""{2}""}}", _ownerId, Guid.NewGuid(), Me.RestToDoTextBox.Text.Replace("""", "'"))
#If Not OPENSILVER Then
                var webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/", "POST", data);
#Else
                'Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
                Dim httpClient = New Http.HttpClient()
                Await httpClient.PostAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/", New Http.StringContent(data, Encoding.UTF8, "application/json"))
#End If
                Await RefreshRestToDos()
            Catch ex As Exception
                Call MessageBox.Show("ERROR: " & ex.ToString())
            End Try

            button.IsEnabled = True
            button.Content = "Create"
        End Sub

        Private Async Sub ButtonDeleteRestToDo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.Content = "Please wait..."
            button.IsEnabled = False

            Try
                Dim todo = CType(button.DataContext, ToDoItem)
#If Not OPENSILVER Then
                var webClient = new WebClient();
                string response = await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString() + "?OwnerId=" + _ownerId.ToString(), "DELETE", "");
#Else
                'Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
                Dim httpClient = New Http.HttpClient()
                Await httpClient.DeleteAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/" & todo.Id.ToString() & "?OwnerId=" & _ownerId.ToString())
#End If

                Await RefreshRestToDos()
            Catch ex As Exception
                Call MessageBox.Show("ERROR: " & ex.ToString())
            End Try

            button.IsEnabled = True
            button.Content = "Delete"
        End Sub

        Private Async Sub ButtonUpdateRestToDo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            Dim todo = CType(button.DataContext, ToDoItem)

            ' Verify that the new description of the To-Do is different from the previous description:
            Dim previousDescription = todo.Description
            Dim newDescription As String = Me.RestToDoTextBox.Text.Replace("""", "'")
            If Equals(newDescription, previousDescription) Then
                MessageBox.Show("To update the To-Do, please enter a different text in the field above, and then click the 'Update' button.")
                Return
            End If

            button.Content = "Please wait..."
            button.IsEnabled = False

            Try
                Dim data = String.Format("{{""OwnerId"": ""{0}"",""Id"": ""{1}"",""Description"": ""{2}""}}", _ownerId, todo.Id, Me.RestToDoTextBox.Text.Replace("""", "'"))
#If Not OPENSILVER Then
                var webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string response = await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString(), "PUT", data);
#Else
                'Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
                Dim httpClient = New Http.HttpClient()
                Await httpClient.PutAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/" & todo.Id.ToString(), New Http.StringContent(data, Encoding.UTF8, "application/json"))
#End If

                Await RefreshRestToDos()
            Catch ex As Exception
                Call MessageBox.Show("ERROR: " & ex.ToString())
            End Try

            button.IsEnabled = True
            button.Content = "Update"
        End Sub
    End Class
End Namespace
