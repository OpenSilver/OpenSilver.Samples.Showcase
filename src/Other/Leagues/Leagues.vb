Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text

Namespace OpenSilver.Samples.Showcase
    Public Class League
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private _name As String
        Private _divisions As ObservableCollection(Of Division)

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property Divisions As ObservableCollection(Of Division)
            Get
                Return _divisions
            End Get
            Set(ByVal value As ObservableCollection(Of Division))
                _divisions = value
            End Set
        End Property

        Private Sub OnPropertyChanged(
<CallerMemberName> ByVal Optional caller As String = Nothing)
            If PropertyChangedEvent IsNot Nothing Then
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(caller))
            End If
        End Sub

        Public Shared Property Leagues As ObservableCollection(Of League) = GetListOfLeagues()

        Public Shared Function GetListOfLeagues() As ObservableCollection(Of League)
            Return New ObservableCollection(Of League)() From {
                New League() With {
                    .Name = "League A",
                    .Divisions = New ObservableCollection(Of Division)() From {
                        New Division() With {
                            .Name = "Division A",
                            .Teams = New ObservableCollection(Of Team)() From {
                                New Team() With {
                                    .Name = "Team 1"
                                },
                                New Team() With {
                                    .Name = "Team 2"
                                },
                                New Team() With {
                                    .Name = "Team 3"
                                },
                                New Team() With {
                                    .Name = "Team 4"
                                }
                            }
                        },
                        New Division() With {
                            .Name = "Division B",
                            .Teams = New ObservableCollection(Of Team)() From {
                                New Team() With {
                                    .Name = "Team 5"
                                },
                                New Team() With {
                                    .Name = "Team 6"
                                },
                                New Team() With {
                                    .Name = "Team 7"
                                },
                                New Team() With {
                                    .Name = "Team 8"
                                }
                            }
                        },
                        New Division() With {
                            .Name = "Division C",
                            .Teams = New ObservableCollection(Of Team)() From {
                                New Team() With {
                                    .Name = "Team 9"
                                },
                                New Team() With {
                                    .Name = "Team 10"
                                },
                                New Team() With {
                                    .Name = "Team 11"
                                },
                                New Team() With {
                                    .Name = "Team 12"
                                }
                            }
                        }
                    }
                },
                New League() With {
                    .Name = "League B",
                    .Divisions = New ObservableCollection(Of Division)() From {
                        New Division() With {
                            .Name = "Division A",
                            .Teams = New ObservableCollection(Of Team)() From {
                                New Team() With {
                                    .Name = "Team 13"
                                },
                                New Team() With {
                                    .Name = "Team 14"
                                },
                                New Team() With {
                                    .Name = "Team 15"
                                },
                                New Team() With {
                                    .Name = "Team 16"
                                }
                            }
                        },
                        New Division() With {
                            .Name = "Division B",
                            .Teams = New ObservableCollection(Of Team)() From {
                                New Team() With {
                                    .Name = "Team 17"
                                },
                                New Team() With {
                                    .Name = "Team 18"
                                },
                                New Team() With {
                                    .Name = "Team 19"
                                },
                                New Team() With {
                                    .Name = "Team 20"
                                }
                            }
                        },
                        New Division() With {
                            .Name = "Division C",
                            .Teams = New ObservableCollection(Of Team)() From {
                                New Team() With {
                                    .Name = "Team 21"
                                },
                                New Team() With {
                                    .Name = "Team 22"
                                },
                                New Team() With {
                                    .Name = "Team 23"
                                },
                                New Team() With {
                                    .Name = "Team 24"
                                }
                            }
                        }
                    }
                }
            }
        End Function
    End Class

    Public Class Division
        Private _name As String
        Private _teams As ObservableCollection(Of Team)

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property Teams As ObservableCollection(Of Team)
            Get
                Return _teams
            End Get
            Set(ByVal value As ObservableCollection(Of Team))
                _teams = value
            End Set
        End Property
    End Class

    Public Class Team
        Private _name As String

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
    End Class
End Namespace
