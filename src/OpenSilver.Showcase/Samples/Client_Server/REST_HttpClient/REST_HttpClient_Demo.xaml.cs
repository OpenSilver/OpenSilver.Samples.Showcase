using DotNetForHtml5.Showcase.SampleRestWebService.Models;
using OpenSilver.Showcase.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    [SearchKeywords("REST", "API", "HttpClient", "HTTP", "web", "service", "request")]
    public partial class REST_HttpClient_Demo : UserControl
    {
        Guid _ownerId;

        public REST_HttpClient_Demo()
        {
            InitializeComponent();

            // The "Owner ID" ensures that every person that uses the Showcase App has its own list of To-Do's:
            _ownerId = Guid.NewGuid();
        }

        async Task RefreshRestToDos()
        {
            try
            {
                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                var httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/xml"));
                var responseMessage = await httpClient.GetAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo?OwnerId=" + _ownerId.ToString());

                string response = await responseMessage.Content.ReadAsStringAsync();

                var dataContractSerializer = new DataContractSerializer(typeof(List<ToDoItem>), new Type[] { typeof(ToDoItem) });
                //convert the string into a stream so it can be deserialized:
                using (var stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(response);
                        writer.Flush();
                        stream.Position = 0;
                        List<ToDoItem> toDoItems = (List<ToDoItem>)dataContractSerializer.ReadObject(stream);
                        RestToDosItemsControl.ItemsSource = toDoItems;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }
        }

        async void ButtonRefreshRestToDos_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Please wait...";
            button.IsEnabled = false;

            await RefreshRestToDos();

            button.IsEnabled = true;
            button.Content = "Refresh the list";
        }

        async void ButtonAddRestToDo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Please wait...";
            button.IsEnabled = false;

            try
            {
                string data = string.Format(@"{{""OwnerId"": ""{0}"",""Id"": ""{1}"",""Description"": ""{2}""}}", _ownerId, Guid.NewGuid(), RestToDoTextBox.Text.Replace("\"", "'"));

                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                var httpClient = new System.Net.Http.HttpClient();
                await httpClient.PostAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/",
                    new System.Net.Http.StringContent(data, Encoding.UTF8, "application/json"));

                await RefreshRestToDos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }

            button.IsEnabled = true;
            button.Content = "Create";
        }

        async void ButtonDeleteRestToDo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Please wait...";
            button.IsEnabled = false;

            try
            {
                ToDoItem todo = ((ToDoItem)button.DataContext);

                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                var httpClient = new System.Net.Http.HttpClient();
                await httpClient.DeleteAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString() + "?OwnerId=" + _ownerId.ToString());

                await RefreshRestToDos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }

            button.IsEnabled = true;
            button.Content = "Delete";
        }

        async void ButtonUpdateRestToDo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            ToDoItem todo = ((ToDoItem)button.DataContext);

            // Verify that the new description of the To-Do is different from the previous description:
            string previousDescription = todo.Description;
            string newDescription = RestToDoTextBox.Text.Replace("\"", "'");
            if (newDescription == previousDescription)
            {
                MessageBox.Show("To update the To-Do, please enter a different text in the field above, and then click the 'Update' button.");
                return;
            }

            button.Content = "Please wait...";
            button.IsEnabled = false;

            try
            {
                string data = string.Format(@"{{""OwnerId"": ""{0}"",""Id"": ""{1}"",""Description"": ""{2}""}}", _ownerId, todo.Id, RestToDoTextBox.Text.Replace("\"", "'"));

                //Note: WebClient is not supported in WebAssembly so we use HttpClient instead
                var httpClient = new System.Net.Http.HttpClient();
                await httpClient.PutAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString(),
                    new System.Net.Http.StringContent(data, Encoding.UTF8, "application/json"));

                await RefreshRestToDos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }

            button.IsEnabled = true;
            button.Content = "Update";
        }
    }
}
