Imports System.Collections.ObjectModel
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase
    Public NotInheritable Class Pages
        Private Shared _allPagesAndCategories As ObservableCollection(Of PageCategoryInfo)
        Private Shared _landingPageInfo As PageInfo
        Private Shared _searchPageInfo As PageInfo

        Public Shared ReadOnly Property AllPagesAndCategories As ObservableCollection(Of PageCategoryInfo)
            Get
                If _allPagesAndCategories Is Nothing Then
                    Dim brush1 = New SolidColorBrush(Color.FromRgb(85, 119, 240))
                    Dim brush2 = New SolidColorBrush(Color.FromRgb(205, 63, 186))
                    Dim brush3 = New SolidColorBrush(Color.FromRgb(253, 163, 28))

                    _allPagesAndCategories = New ObservableCollection(Of PageCategoryInfo) From {
                        New PageCategoryInfo With {
                            .Name = "XAML & UI",
                            .Foreground = brush1,
                            .Pages = New ObservableCollection(Of PageInfo) From {
                                New PageInfo With {.Name = "Controls", .Path = "/XAML_Controls", .Icon = ChrW(&HE913), .IconBrush = brush1},
                                New PageInfo With {.Name = "Data Controls", .Path = "/Data_Controls", .Icon = ChrW(&HF1D0), .IconBrush = brush1},
                                New PageInfo With {.Name = "XAML Features", .Path = "/XAML_Features", .Icon = ChrW(&HE920), .IconBrush = brush1},
                                New PageInfo With {.Name = "Layout", .Path = "/XAML_Layout", .Icon = ChrW(&HE66B), .IconBrush = brush1},
                                New PageInfo With {.Name = "JS Libs", .Path = "/JS_Libs", .Icon = ChrW(&HEB7C), .IconBrush = brush1},
                                New PageInfo With {.Name = "Charts", .Path = "/Charts", .Icon = ChrW(&HE24B), .IconBrush = brush1}
                            }
                        },
                        New PageCategoryInfo With {
                            .Name = "NON-UI",
                            .Foreground = brush2,
                            .Pages = New ObservableCollection(Of PageInfo) From {
                                New PageInfo With {.Name = "Client / Server", .Path = "/Client_Server", .Icon = ChrW(&HE1E2), .IconBrush = brush2},
                                New PageInfo With {.Name = ".NET Framework", .Path = "/Net_Framework", .Icon = ChrW(&HE1BD), .IconBrush = brush2},
                                New PageInfo With {.Name = "Native APIs", .Path = "/Maui_Hybrid", .Icon = ChrW(&HE0D4), .IconBrush = brush2}
                            }
                        },
                        New PageCategoryInfo With {
                            .Name = "OTHER",
                            .Foreground = brush3,
                            .Pages = New ObservableCollection(Of PageInfo) From {
                                New PageInfo With {.Name = "Interop", .Path = "/Interop_Samples", .Icon = ChrW(&HEACD), .IconBrush = brush3},
                                New PageInfo With {.Name = "Performance", .Path = "/Performance", .Icon = ChrW(&HEB9B), .IconBrush = brush3},
                                New PageInfo With {.Name = "Third-Party", .Path = "/Third_Party", .Icon = ChrW(&HEBBB), .IconBrush = brush3},
                                LandingPageInfo,
                                SearchPageInfo
                            }
                        }
                    }
                End If
                Return _allPagesAndCategories
            End Get
        End Property

        Public Shared ReadOnly Property AllPages As IEnumerable(Of PageInfo)
            Get
                Return _allPagesAndCategories.SelectMany(Function(c) If(c.Pages, Enumerable.Empty(Of PageInfo)()))
            End Get
        End Property

        Public Shared ReadOnly Property LandingPageInfo As PageInfo
            Get
                If _landingPageInfo Is Nothing Then
                    _landingPageInfo = New PageInfo With {.Name = "Home", .Path = "/Welcome", .IsVisibleInMenu = False}
                End If
                Return _landingPageInfo
            End Get
        End Property

        Public Shared ReadOnly Property SearchPageInfo As PageInfo
            Get
                If _searchPageInfo Is Nothing Then
                    _searchPageInfo = New PageInfo With {.Name = "Search", .Path = "/Search", .IsVisibleInMenu = False}
                End If
                Return _searchPageInfo
            End Get
        End Property
    End Class

End Namespace
