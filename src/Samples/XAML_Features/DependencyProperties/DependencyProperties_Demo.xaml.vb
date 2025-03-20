﻿Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("properties", "dependency property", "binding", "XAML", "UI")>
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
    End Class
End Namespace
