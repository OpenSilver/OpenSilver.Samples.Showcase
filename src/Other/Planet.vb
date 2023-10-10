Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace Global.OpenSilver.Samples.Showcase
    Public Enum PlanetStructure
        Rock
        Gas
    End Enum

    Public Class Planet
        Implements INotifyPropertyChanged
        Private _name As String
        Private _radius As Integer
        Private _structure As PlanetStructure
        Private _bright As Boolean
        Private _rotationPeriod As String
        Private _orbitalPeriod As String
        Private _imagePath As String

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
                OnPropertyChanged("Name")
            End Set
        End Property
        Public Property Radius As Integer
            Get
                Return _radius
            End Get
            Set(ByVal value As Integer)
                _radius = value
                OnPropertyChanged("Radius")
            End Set
        End Property
        Public Property [Structure] As PlanetStructure
            Get
                Return _structure
            End Get
            Set(ByVal value As PlanetStructure)
                _structure = value
                OnPropertyChanged("Structure")
            End Set
        End Property
        Public Property Bright As Boolean
            Get
                Return _bright
            End Get
            Set(ByVal value As Boolean)
                _bright = value
                OnPropertyChanged("Bright")
            End Set
        End Property
        Public Property RotationPeriod As String
            Get
                Return _rotationPeriod
            End Get
            Set(ByVal value As String)
                _rotationPeriod = value
                OnPropertyChanged("RotationPeriod")
            End Set
        End Property
        Public Property OrbitalPeriod As String
            Get
                Return _orbitalPeriod
            End Get
            Set(ByVal value As String)
                _orbitalPeriod = value
                OnPropertyChanged("OrbitalPeriod")
            End Set
        End Property
        Public Property ImagePath As String
            Get
                Return _imagePath
            End Get
            Set(ByVal value As String)
                _imagePath = value
                OnPropertyChanged("ImagePath")
            End Set
        End Property

        Public Shared Property Planets As ObservableCollection(Of Planet) = GetListOfPlanets()

        Public Shared Function GetListOfPlanets() As ObservableCollection(Of Planet)
            Return New ObservableCollection(Of Planet)() From {
    New Planet() With {
        .Name = "Mercury",
        .[Structure] = PlanetStructure.Rock,
        .Bright = True,
        .Radius = 2400,
        .RotationPeriod = "59 days",
        .OrbitalPeriod = "3 months",
        .ImagePath = "ms-appx:/Other/Planets/Mercury.png"
    },
    New Planet() With {
        .Name = "Venus",
        .[Structure] = PlanetStructure.Rock,
        .Bright = True,
        .Radius = 6100,
        .RotationPeriod = "243 days",
        .OrbitalPeriod = "7 months",
        .ImagePath = "ms-appx:/Other/Planets/Venus.png"
    },
    New Planet() With {
        .Name = "Earth",
        .[Structure] = PlanetStructure.Rock,
        .Bright = True,
        .Radius = 6400,
        .RotationPeriod = "1 day",
        .OrbitalPeriod = "1 year",
        .ImagePath = "ms-appx:/Other/Planets/Earth.png"
    },
    New Planet() With {
        .Name = "Mars",
        .[Structure] = PlanetStructure.Rock,
        .Bright = True,
        .Radius = 3400,
        .RotationPeriod = "1 day, 37 min",
        .OrbitalPeriod = "2 years",
        .ImagePath = "ms-appx:/Other/Planets/Mars.png"
    },
    New Planet() With {
        .Name = "Jupiter",
        .[Structure] = PlanetStructure.Gas,
        .Bright = True,
        .Radius = 71500,
        .RotationPeriod = "1 day, 10 hrs",
        .OrbitalPeriod = "12 years",
        .ImagePath = "ms-appx:/Other/Planets/Jupiter.png"
    },
    New Planet() With {
        .Name = "Saturn",
        .[Structure] = PlanetStructure.Gas,
        .Bright = True,
        .Radius = 60300,
        .RotationPeriod = "1 day, 11 hrs",
        .OrbitalPeriod = "30 years",
        .ImagePath = "ms-appx:/Other/Planets/Saturn.png"
    },
    New Planet() With {
        .Name = "Uranus",
        .[Structure] = PlanetStructure.Gas,
        .Bright = False,
        .Radius = 25600,
        .RotationPeriod = "1 day, 17 hrs",
        .OrbitalPeriod = "84 years",
        .ImagePath = "ms-appx:/Other/Planets/Uranus.png"
    },
    New Planet() With {
        .Name = "Neptune",
        .[Structure] = PlanetStructure.Gas,
        .Bright = False,
        .Radius = 24800,
        .RotationPeriod = "1 day, 16 hrs",
        .OrbitalPeriod = "165 years",
        .ImagePath = "ms-appx:/Other/Planets/Neptune.png"
    }
}
        End Function

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub OnPropertyChanged(
<CallerMemberName> ByVal Optional caller As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(caller))
        End Sub
    End Class
End Namespace
