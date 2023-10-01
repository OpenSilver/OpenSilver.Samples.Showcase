#If OPENSILVER Then
Imports ServiceReference1
#Else
using OpenSilver.Samples.Showcase.ServiceReference1;
#End If
Imports System
Imports System.Collections.Generic
Imports System.ServiceModel
Imports System.Threading.Tasks
Imports System.Windows
#If SLMIGRATION Then
Imports System.Windows.Controls
#Else
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class WCF_SOAP_Demo
        Inherits UserControl
        Private _ownerId As Guid

        Private Shared ReadOnly EndpointConfig As Service1Client.EndpointConfiguration = Service1Client.EndpointConfiguration.BasicHttpsBinding_IService1

        Public Sub New()
            Me.InitializeComponent()

            ' The "Owner ID" ensures that every person that uses the Showcase App has its own list of To-Do's:
            _ownerId = Guid.NewGuid()
        End Sub

        Private Async Function RefreshSoapToDos() As Task
            Try
#If OPENSILVER Then
                Dim soapClient As Service1Client = New Service1Client(EndpointConfig)
                Dim result = Await soapClient.GetToDosAsync(_ownerId.ToString())
#Else
                Service1Client soapClient = new Service1Client();
                var result = await soapClient.GetToDosAsync(_ownerId);
#End If
                Dim todos = result.Body.GetToDosResult
                Me.SoapToDosItemsControl.ItemsSource = todos
            Catch ex As Exception
                Call MessageBox.Show("ERROR: " & ex.ToString())
            End Try
        End Function

        Private Async Sub ButtonRefreshSoapToDos_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.Content = "Please wait..."
            button.IsEnabled = False

            Await RefreshSoapToDos()

            button.IsEnabled = True
            button.Content = "Refresh the list"
        End Sub

        Private Async Sub ButtonAddSoapToDo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.Content = "Please wait..."
            button.IsEnabled = False

            Try
#If OPENSILVER Then
                Dim todo As ToDoItem = New ToDoItem() With {
    .Description = Me.SoapToDoTextBox.Text,
    .Id = Guid.NewGuid().ToString(),
    .OwnerId = _ownerId.ToString()
    }
#Else
                Dim todo As ToDoItem = New ToDoItem() With {
                .Description = Me.SoapToDoTextBox.Text,
                   .Id = Guid.NewGuid(),
                    .OwnerId = _ownerId
                }
#End If

#If OPENSILVER Then
                Dim soapClient As Service1Client = New Service1Client(EndpointConfig)
#Else
                Dim soapClient As New Service1Client()
#End If

                Await soapClient.AddOrUpdateToDoAsync(todo)

                Await RefreshSoapToDos()
            Catch ex As Exception
                Call MessageBox.Show("ERROR: " & ex.ToString())
            End Try

            button.IsEnabled = True
            button.Content = "Create"
        End Sub

        Private Async Sub ButtonDeleteSoapToDo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.Content = "Please wait..."
            button.IsEnabled = False

            Try
                Dim todo = CType(CType(sender, Button).DataContext, ToDoItem)

#If OPENSILVER Then
                Dim soapClient As Service1Client = New Service1Client(EndpointConfig)
#Else
                Service1Client soapClient = new Service1Client();
#End If

                Await soapClient.DeleteToDoAsync(todo)

                Await RefreshSoapToDos()
            Catch ex As FaultException
                ' With "Fault Exceptions" the server can pass to the client some deliberate information such as "Item not found":
                MessageBox.Show(ex.Message)
            End Try

            button.IsEnabled = True
            button.Content = "Delete"
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WCF_SOAP_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WCF_SOAP_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "WCF_SOAP_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Client_Server/WCF_SOAP/WCF_SOAP_Demo.xaml.vb"
    }
})
        End Sub


    End Class
End Namespace
