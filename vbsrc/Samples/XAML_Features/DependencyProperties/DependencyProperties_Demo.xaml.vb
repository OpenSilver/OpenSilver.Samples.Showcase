Imports System.Collections.Generic
#If SLMIGRATION Then
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.VBShowcase
    Partial Public Class DependencyProperties_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Public Property MySampleDependencyProperty As Integer
            Get
                Return GetValue(MySampleDependencyPropertyProperty)
            End Get
            Set(ByVal value As Integer)
                SetValue(MySampleDependencyPropertyProperty, value)
            End Set
        End Property

        ' Using a DependencyProperty as the backing store for MySampleDependencyProperty. This enables animation, styling, binding, etc...
        Public Shared ReadOnly MySampleDependencyPropertyProperty As DependencyProperty = DependencyProperty.Register("MySampleDependencyProperty", GetType(Integer), GetType(DependencyProperties_Demo), New PropertyMetadata(0, AddressOf DependencyProperty_Changed) With {.CallPropertyChangedWhenLoadedIntoVisualTree = WhenToCallPropertyChangedEnum.Never})

        Private Shared Sub DependencyProperty_Changed(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            MessageBox.Show("The DependencyProperty value has changed!")
        End Sub

        Public Sub Decrementation_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MySampleDependencyProperty -= 1
        End Sub

        Public Sub Incrementation_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MySampleDependencyProperty += 1
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DependencyProperties_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DependencyProperties_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml.cs"
    },
                   New ViewSourceButtonInfo() With {
        .TabHeader = "DependencyProperties_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml.vb"
    }
})
        End Sub
    End Class
End Namespace
