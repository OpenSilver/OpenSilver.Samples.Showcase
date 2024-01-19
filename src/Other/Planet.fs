namespace OpenSilver.Samples.Showcase

open System.Collections.ObjectModel
open System.ComponentModel
open System.Runtime.CompilerServices

type PlanetStructure =
    | Rock
    | Gas

type Planet() =
    let mutable _name = ""
    let mutable _radius = 0
    let mutable _structure = Rock
    let mutable _bright = false
    let mutable _rotationPeriod = ""
    let mutable _orbitalPeriod = ""
    let mutable _imagePath = ""

    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()
    member this.PropertyChanged = propertyChanged.Publish

    member this.Name
        with get() = _name
        and set(value) =
            _name <- value
            this.OnPropertyChanged("Name")

    member this.Radius
        with get() = _radius
        and set(value) =
            _radius <- value
            this.OnPropertyChanged("Radius")

    member this.Structure
        with get() = _structure
        and set(value) =
            _structure <- value
            this.OnPropertyChanged("Structure")

    member this.Bright
        with get() = _bright
        and set(value) =
            _bright <- value
            this.OnPropertyChanged("Bright")

    member this.RotationPeriod
        with get() = _rotationPeriod
        and set(value) =
            _rotationPeriod <- value
            this.OnPropertyChanged("RotationPeriod")

    member this.OrbitalPeriod
        with get() = _orbitalPeriod
        and set(value) =
            _orbitalPeriod <- value
            this.OnPropertyChanged("OrbitalPeriod")

    member this.ImagePath
        with get() = _imagePath
        and set(value) =
            _imagePath <- value
            this.OnPropertyChanged("ImagePath")

    static member Planets =
        Planet.GetListOfPlanets()

    static member GetListOfPlanets() =
        ObservableCollection<Planet>(
            [
                new Planet(Name = "Mercury", Structure = Rock, Bright = true, Radius = 2400, RotationPeriod = "59 days", OrbitalPeriod = "3 months", ImagePath = "ms-appx:/Other/Planets/Mercury.png")
                new Planet(Name = "Venus", Structure = Rock, Bright = true, Radius = 6100, RotationPeriod = "243 days", OrbitalPeriod = "7 months", ImagePath = "ms-appx:/Other/Planets/Venus.png")
                new Planet(Name = "Earth", Structure = Rock, Bright = true, Radius = 6400, RotationPeriod = "1 day", OrbitalPeriod = "1 year", ImagePath = "ms-appx:/Other/Planets/Earth.png")
                new Planet(Name = "Mars", Structure = Rock, Bright = true, Radius = 3400, RotationPeriod = "1 day, 37 min", OrbitalPeriod = "2 years", ImagePath = "ms-appx:/Other/Planets/Mars.png")
                new Planet(Name = "Jupiter", Structure = Gas, Bright = true, Radius = 71500, RotationPeriod = "1 day, 10 hrs", OrbitalPeriod = "12 years", ImagePath = "ms-appx:/Other/Planets/Jupiter.png")
                new Planet(Name = "Saturn", Structure = Gas, Bright = true, Radius = 60300, RotationPeriod = "1 day, 11 hrs", OrbitalPeriod = "30 years", ImagePath = "ms-appx:/Other/Planets/Saturn.png")
                new Planet(Name = "Uranus", Structure = Gas, Bright = false, Radius = 25600, RotationPeriod = "1 day, 17 hrs", OrbitalPeriod = "84 years", ImagePath = "ms-appx:/Other/Planets/Uranus.png")
                new Planet(Name = "Neptune", Structure = Gas, Bright = false, Radius = 24800, RotationPeriod = "1 day, 16 hrs", OrbitalPeriod = "165 years", ImagePath = "ms-appx:/Other/Planets/Neptune.png")
            ])

    member private this.OnPropertyChanged(propertyName : string) =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(propertyName))


