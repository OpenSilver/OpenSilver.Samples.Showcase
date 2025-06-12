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
                    _allPagesAndCategories = New ObservableCollection(Of PageCategoryInfo) From {
                        New PageCategoryInfo With {
                            .Name = "XAML & UI",
                            .Foreground = New SolidColorBrush(Color.FromRgb(85, 119, 240)),
                            .Pages = New ObservableCollection(Of PageInfo) From {
                                New PageInfo With {.Name = "Controls", .Path = "/XAML_Controls", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = "XAML Features", .Path = "/XAML_Features", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = "Layout", .Path = "/XAML_Layout", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = "JS Libs", .Path = "/JS_Libs", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = "Charts", .Path = "/Charts", .IsVisibleInMenu = True}
                            }
                        },
                        New PageCategoryInfo With {
                            .Name = "NON-UI",
                            .Foreground = New SolidColorBrush(Color.FromRgb(205, 63, 186)),
                            .Pages = New ObservableCollection(Of PageInfo) From {
                                New PageInfo With {.Name = "Client / Server", .Path = "/Client_Server", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = ".NET Framework", .Path = "/Net_Framework", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = "Native APIs", .Path = "/Maui_Hybrid", .IsVisibleInMenu = True}
                            }
                        },
                        New PageCategoryInfo With {
                            .Name = "OTHER",
                            .Foreground = New SolidColorBrush(Color.FromRgb(253, 163, 28)),
                            .Pages = New ObservableCollection(Of PageInfo) From {
                                New PageInfo With {.Name = "Interop", .Path = "/Interop_Samples", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = "Performance", .Path = "/Performance", .IsVisibleInMenu = True},
                                New PageInfo With {.Name = "Third-Party", .Path = "/Third_Party", .IsVisibleInMenu = True},
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
