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
    public partial class WCF_SOAP_Demo : UserControl
    {
        Guid _ownerId;

        public WCF_SOAP_Demo()
        {
            this.InitializeComponent();

            // The "Owner ID" ensures that every person that uses the Showcase App has its own list of To-Do's:
            _ownerId = Guid.NewGuid();
        }

        async Task RefreshSoapToDos()
        {
            Service1Client soapClient = new Service1Client();
            var result = await soapClient.GetToDosAsync(_ownerId);
            ToDoItem[] todos = result.Body.GetToDosResult;
            SoapToDosItemsControl.ItemsSource = todos;
        }

        async void ButtonRefreshSoapToDos_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Please wait...";
            button.IsEnabled = false;

            await RefreshSoapToDos();

            button.IsEnabled = true;
            button.Content = "Refresh the list";
        }

        async void ButtonAddSoapToDo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Please wait...";
            button.IsEnabled = false;

            ToDoItem todo = new ToDoItem()
            {
                Description = SoapToDoTextBox.Text,
                Id = Guid.NewGuid(),
                OwnerId = _ownerId
            };

            Service1Client soapClient = new Service1Client();

            await soapClient.AddOrUpdateToDoAsync(todo);

            await RefreshSoapToDos();

            button.IsEnabled = true;
            button.Content = "Create";
        }

        async void ButtonDeleteSoapToDo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "Please wait...";
            button.IsEnabled = false;

            try
            {
                ToDoItem todo = (ToDoItem)((Button)sender).DataContext;

                Service1Client soapClient = new Service1Client();

                await soapClient.DeleteToDoAsync(todo);

                await RefreshSoapToDos();
            }
            catch (FaultException ex)
            {
                // With "Fault Exceptions" the server can pass to the client some deliberate information such as "Item not found":
                MessageBox.Show(ex.Message);
            }

            button.IsEnabled = true;
            button.Content = "Delete";
        }

    }
}