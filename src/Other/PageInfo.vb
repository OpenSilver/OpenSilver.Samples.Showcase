Namespace OpenSilver.Samples.Showcase
    Public Class PageInfo
        Public Sub New(name As String, path As String)
            Me.Name = name
            Me.Path = path
        End Sub

        Private Shared _pageInfos As List(Of PageInfo)
        Private Shared _landingPageInfo As PageInfo
        Private Shared _searchPageInfo As PageInfo

        Public Shared ReadOnly Property Pages As List(Of PageInfo)
            Get
                If _pageInfos Is Nothing Then
                    _pageInfos = New List(Of PageInfo) From {
                        New PageInfo("Panels & Controls", "/XAML_Controls"),
                        New PageInfo("Xaml Features", "/XAML_Features"),
                        New PageInfo(".NET Framework", "/Net_Framework"),
                        New PageInfo("Client / Server", "/Client_Server"),
                        New PageInfo("Interop", "/Interop_Samples"),
                        New PageInfo("Charts", "/Charts"),
                        New PageInfo("Performance", "/Performance"),
                        New PageInfo("Native APIs", "/Maui_Hybrid"),
                        New PageInfo("Third-Party", "/Third_Party"),
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
                    _landingPageInfo = New PageInfo("Home", "/Welcome")
                End If
                Return _landingPageInfo
            End Get
        End Property

        Public Shared ReadOnly Property SearchPageInfo As PageInfo
            Get
                If _searchPageInfo Is Nothing Then
                    _searchPageInfo = New PageInfo("Search", "/Search")
                End If
                Return _searchPageInfo
            End Get
        End Property

        Public Property Name As String
        Public Property Path As String
    End Class
End Namespace
