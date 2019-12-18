using CSHTML5.Samples.Showcase.ServiceReference1;
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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CSHTML5.Samples.Showcase
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
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers[HttpRequestHeader.Accept] = "application/xml";

            string response = await webClient.DownloadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo?OwnerId=" + _ownerId.ToString());

            var dataContractSerializer = new DataContractSerializer(typeof(List<ToDoItem>));
            List<ToDoItem> toDoItems = (List<ToDoItem>)dataContractSerializer.DeserializeFromString(response);
            RestToDosItemsControl.ItemsSource = toDoItems;
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

            string data = string.Format(@"{{""OwnerId"": ""{0}"",""Id"": ""{1}"",""Description"": ""{2}""}}", _ownerId, Guid.NewGuid(), RestToDoTextBox.Text.Replace("\"", "'"));
            var webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            string response = await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/", "POST", data);

            await RefreshRestToDos();

            button.IsEnabled = true;
            button.Content = "Create";
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
                todo = new ToDoItem();
                string data = string.Format(@"{{""OwnerId"": ""{0}"",""Id"": ""{1}"",""Description"": ""{2}""}}", _ownerId, todo.Id, RestToDoTextBox.Text.Replace("\"", "'"));
                var webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                string response = await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString(), "PUT", data);

                await RefreshRestToDos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }

            button.IsEnabled = true;
            button.Content = "Update";
        }

        async void ButtonDeleteRestToDo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Please wait...";
            button.IsEnabled = false;

            ToDoItem todo = ((ToDoItem)button.DataContext);
            var webClient = new WebClient();
            string response = await webClient.UploadStringTaskAsync("http://cshtml5-rest-sample.azurewebsites.net/api/Todo/" + todo.Id.ToString() + "?OwnerId=" + _ownerId.ToString(), "DELETE", "");

            await RefreshRestToDos();

            button.IsEnabled = true;
            button.Content = "Delete";
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "REST_WebClient_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Client_Server/REST_WebClient/REST_WebClient_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "REST_WebClient_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Client_Server/REST_WebClient/REST_WebClient_Demo.xaml.cs"
                }
            });
        }

    }
}
