#if !OPENSILVER
using OpenSilver.Samples.Showcase.ServiceReference1;
#else
using DotNetForHtml5.Showcase.SampleRestWebService.Models;
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
#if SLMIGRATION
using System.Windows.Controls;
#else
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class REST_WebClient_Demo : UserControl
    {
        Guid _ownerId;

        public REST_WebClient_Demo()
        {
            this.InitializeComponent();

            // The "Owner ID" ensures that every person that uses the Showcase App has its own list of To-Do's:
            _ownerId = Guid.NewGuid();
        }

        async Task RefreshRestToDos()
        {
            try
            {
#if !OPENSILVER
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers[HttpRequestHeader.Accept] = "application/xml";
                string response = await webClient.DownloadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo?OwnerId=" + _ownerId.ToString());
                var dataContractSerializer = new DataContractSerializer(typeof(List<ToDoItem>));
                List<ToDoItem> toDoItems = (List<ToDoItem>)dataContractSerializer.DeserializeFromString(response);
                RestToDosItemsControl.ItemsSource = toDoItems;
#else
                //Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
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
#endif
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
#if !OPENSILVER
                var webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/", "POST", data);
#else
                //Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
                var httpClient = new System.Net.Http.HttpClient();
                await httpClient.PostAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/",
                    new System.Net.Http.StringContent(data,Encoding.UTF8, "application/json"));
#endif
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
#if !OPENSILVER
                var webClient = new WebClient();
                string response = await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString() + "?OwnerId=" + _ownerId.ToString(), "DELETE", "");
#else
                //Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
                var httpClient = new System.Net.Http.HttpClient();
                await httpClient.DeleteAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString() + "?OwnerId=" + _ownerId.ToString());
#endif

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
#if !OPENSILVER
                var webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string response = await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString(), "PUT", data);
#else
                //Note: it seems WebClient is not supported (despite existing) in Blazor so we use HttpClient instead
                var httpClient = new System.Net.Http.HttpClient();
                await httpClient.PutAsync("https://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString(), 
                    new System.Net.Http.StringContent(data, Encoding.UTF8, "application/json"));
#endif

                await RefreshRestToDos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }

            button.IsEnabled = true;
            button.Content = "Update";
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "REST_WebClient_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/REST_WebClient/REST_WebClient_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "REST_WebClient_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/REST_WebClient/REST_WebClient_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "REST_ToDoItem.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/REST_WebClient/REST_ToDoItem.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "REST_WebClient_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/REST_WebClient/REST_WebClient_Demo.xaml.vb"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "REST_ToDoItem.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/REST_WebClient/REST_ToDoItem.vb"
                }
            });
        }

    }
}
