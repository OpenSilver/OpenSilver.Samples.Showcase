Imports System.IO
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Showcase
    Public Partial Class ViewSourcePanel
        Inherits Grid

        Private _sources As IEnumerable(Of ViewSourceButtonInfo)

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Sub ViewSource(sources As IEnumerable(Of ViewSourceButtonInfo))
            _sources = sources
            cSharpButton.IsChecked = True
        End Sub

        Private Sub UpdateTabs(ByVal sources As IEnumerable(Of ViewSourceButtonInfo))
            Dim selectedIndex = TabControl.SelectedIndex
            TabControl.Items.Clear()

            For Each viewSourceButtonInfo In sources
                TabControl.Items.Add(New TabItem() With {
                    .Header = viewSourceButtonInfo.GetHeader(),
                    .Content = New ControlToDisplayCodeHostedOnGitHub(viewSourceButtonInfo.GetAbsoluteUrl()),
                    .DataContext = viewSourceButtonInfo
                })
            Next

            If selectedIndex >= 0 AndAlso TabControl.Items.Count > selectedIndex Then
                TabControl.SelectedIndex = selectedIndex
            End If
        End Sub

        Private Function GetCSharpSources() As IEnumerable(Of ViewSourceButtonInfo)
            Return GetSources(".vb", ".fs")
        End Function

        Private Function GetVBNETSources() As IEnumerable(Of ViewSourceButtonInfo)
            Return GetSources(".cs", ".fs")
        End Function

        Private Function GetFSharpSources() As IEnumerable(Of ViewSourceButtonInfo)
            Return GetSources(".cs", ".vb")
        End Function

        Private Function GetSources(ParamArray extensionsToIgnore As String()) As IEnumerable(Of ViewSourceButtonInfo)
            Return _sources.Where(Function(x) String.IsNullOrEmpty(x.FileName) OrElse Not extensionsToIgnore.Contains(Path.GetExtension(x.FileName)?.ToLower()))
        End Function

        Private Sub OnCSharpRadioButtonChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateTabs(GetCSharpSources())
        End Sub

        Private Sub OnVBNETRadioButtonChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateTabs(GetVBNETSources())
        End Sub

        Private Sub OnFSharpRadioButtonChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateTabs(GetFSharpSources())
        End Sub
    End Class
End Namespace
