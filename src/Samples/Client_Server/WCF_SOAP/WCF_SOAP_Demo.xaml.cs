#if OPENSILVER
using ServiceReference1;
#else
using OpenSilver.Samples.Showcase.ServiceReference1;
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
            try
            {
#if OPENSILVER
                Service1Client soapClient = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
                var result = await soapClient.GetToDosAsync(_ownerId.ToString());
#else
                Service1Client soapClient = new Service1Client();
                var result = await soapClient.GetToDosAsync(_ownerId);
#endif
                ToDoItem[] todos = result.Body.GetToDosResult;
                SoapToDosItemsControl.ItemsSource = todos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }
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

            try
            {
                ToDoItem todo = new ToDoItem()
                {
                    Description = SoapToDoTextBox.Text,
#if OPENSILVER
                    Id = Guid.NewGuid().ToString(),
                    OwnerId = _ownerId.ToString()
#else
                    Id = Guid.NewGuid(),
                    OwnerId = _ownerId
#endif
                };
#if OPENSILVER
                Service1Client soapClient = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
#else
                Service1Client soapClient = new Service1Client();
#endif

                await soapClient.AddOrUpdateToDoAsync(todo);

                await RefreshSoapToDos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }

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

#if OPENSILVER
                Service1Client soapClient = new Service1Client(Service1Client.EndpointConfiguration.BasicHttpBinding_IService1);
#else
                Service1Client soapClient = new Service1Client();
#endif

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

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WCF_SOAP_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WCF_SOAP_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml.cs"
                }
            });
        }


    }
}