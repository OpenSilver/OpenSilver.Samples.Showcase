Imports System.Windows.Controls
Imports System.Windows

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Net_Framework
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

#If OPENSILVER
            'File_OpenDemo.Visibility = Visibility.Collapsed;
            'File_SaveDemo.Visibility = Visibility.Collapsed;
            'ZipDemo.Visibility = Visibility.Collapsed;
            'IsolatedStorageFileDemo.Visibility = Visibility.Collapsed;
            'IsolatedStorageSettingsDemo.Visibility = Visibility.Collapsed;
            Me.JSON_SerializerDemo.Visibility = Visibility.Collapsed
            Me.GetRessourceStreamDemo.Visibility = Visibility.Collapsed
            'FullScreenDemo.Visibility = Visibility.Collapsed;
            Me.ConsoleDemo.Visibility = Visibility.Collapsed
#End If
            Me.RESXDemo.Visibility = Visibility.Collapsed
        End Sub
    End Class
End Namespace
