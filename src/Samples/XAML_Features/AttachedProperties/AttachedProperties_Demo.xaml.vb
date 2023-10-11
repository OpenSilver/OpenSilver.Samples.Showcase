﻿Imports System.Collections.Generic
#If SLMIGRATION
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

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class AttachedProperties_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "SampleAttachedProperties.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/AttachedProperties/SampleAttachedProperties.cs"
    }, New ViewSourceButtonInfo() With {
        .TabHeader = "SampleAttachedProperties.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/AttachedProperties/SampleAttachedProperties.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "AttachedProperties_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/AttachedProperties/AttachedProperties_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "AttachedProperties_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/AttachedProperties/AttachedProperties_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "AttachedProperties_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/AttachedProperties/AttachedProperties_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace