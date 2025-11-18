using OpenSilver.Showcase.Search;
using ServiceReference1;
using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    [SearchKeywords("WCF", "SOAP", "web", "service", "communication", "network")]
    public partial class WCF_SOAP_Demo : UserControl
    {
        Guid _ownerId;

        private static readonly Service1Client.EndpointConfiguration EndpointConfig =
            Service1Client.EndpointConfiguration.BasicHttpsBinding_IService1;

        public WCF_SOAP_Demo()
        {
            InitializeComponent();

            // The "Owner ID" ensures that every person that uses the Showcase App has its own list of To-Do's:
            _ownerId = Guid.NewGuid();
        }

        async Task RefreshSoapToDos()
        {
            try
            {
                Service1Client soapClient = new Service1Client(EndpointConfig);
                var result = await soapClient.GetToDosAsync(_ownerId.ToString());

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
                    Id = Guid.NewGuid().ToString(),
                    OwnerId = _ownerId.ToString()
                };

                Service1Client soapClient = new Service1Client(EndpointConfig);

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

                Service1Client soapClient = new Service1Client(EndpointConfig);

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