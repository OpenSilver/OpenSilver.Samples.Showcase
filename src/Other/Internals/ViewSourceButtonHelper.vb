Imports System.Collections.Generic

#If SLMIGRATION Then
Imports System.Windows
Imports System.Windows.Controls
#Else
Imports Windows.UI.Xaml
Imports Windows.UI.Xaml.Controls
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Friend Module ViewSourceButtonHelper
        Public Sub ViewSource(ByVal sourcePaths As List(Of ViewSourceButtonInfo))
            ViewSourceImpl(sourcePaths)
        End Sub

        Public Sub ViewSource(ParamArray sourcePaths As ViewSourceButtonInfo())
            ViewSourceImpl(sourcePaths)
        End Sub

        Private Sub ViewSourceImpl(ByVal sourcePaths As ICollection(Of ViewSourceButtonInfo))
            If sourcePaths Is Nothing OrElse sourcePaths.Count = 0 Then
                Return
            End If

            Dim tabControl = New TabControl()

            For Each viewSourceButtonInfo As ViewSourceButtonInfo In sourcePaths
                tabControl.Items.Add(New TabItem() With {
                    .Header = viewSourceButtonInfo.GetHeader(),
                    .Content = New ControlToDisplayCodeHostedOnGitHub(viewSourceButtonInfo.GetAbsoluteUrl())
                })
            Next

            CType(tabControl.Items(0), TabItem).IsSelected = True

            MainPage.Current.ViewSourceCode(tabControl)
        End Sub
    End Module
End Namespace
