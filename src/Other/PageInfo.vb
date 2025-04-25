Namespace OpenSilver.Samples.Showcase

    Public Class PageInfo

        Public Sub New(name As String, path As String, isVisibleInMenu As Boolean)
            Me.Name = name
            Me.Path = path
            Me.IsVisibleInMenu = isVisibleInMenu
        End Sub

        Private Shared _pageInfos As List(Of PageInfo)
        Private Shared _landingPageInfo As PageInfo
        Private Shared _searchPageInfo As PageInfo

        Public Shared ReadOnly Property Pages As List(Of PageInfo)
            Get
                If _pageInfos Is Nothing Then
                    _pageInfos = New List(Of PageInfo) From {
                        New PageInfo("Panels & Controls", "/XAML_Controls", True),
                        New PageInfo("Xaml Features", "/XAML_Features", True),
                        New PageInfo(".NET Framework", "/Net_Framework", True),
                        New PageInfo("Client / Server", "/Client_Server", True),
                        New PageInfo("Interop", "/Interop_Samples", True),
                        New PageInfo("JS Libs", "/JS_Libs", True),
                        New PageInfo("Charts", "/Charts", True),
                        New PageInfo("Performance", "/Performance", True),
                        New PageInfo("Native APIs", "/Maui_Hybrid", True),
                        New PageInfo("Third-Party", "/Third_Party", True),
                        LandingPageInfo,
                        SearchPageInfo
                    }
                End If
                Return _pageInfos
            End Get
        End Property

        Public Shared ReadOnly Property LandingPageInfo As PageInfo
            Get
                If _landingPageInfo Is Nothing Then
                    _landingPageInfo = New PageInfo("Home", "/Welcome", False)
                End If
                Return _landingPageInfo
            End Get
        End Property

        Public Shared ReadOnly Property SearchPageInfo As PageInfo
            Get
                If _searchPageInfo Is Nothing Then
                    _searchPageInfo = New PageInfo("Search", "/Search", False)
                End If
                Return _searchPageInfo
            End Get
        End Property

        Public Property Name As String
        Public Property Path As String
        Public Property IsVisibleInMenu As Boolean

    End Class

End Namespace
