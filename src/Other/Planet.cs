using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenSilver.Samples.Showcase
{
    public enum PlanetStructure
    {
        Rock, Gas
    }

    public class Planet : INotifyPropertyChanged
    {
        string _name;
        int _radius;
        PlanetStructure _structure;
        bool _bright;
        string _rotationPeriod;
        string _orbitalPeriod;
        string _imagePath;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; OnPropertyChanged("Radius"); }
        }
        public PlanetStructure Structure
        {
            get { return _structure; }
            set { _structure = value; OnPropertyChanged("Structure"); }
        }
        public bool Bright
        {
            get { return _bright; }
            set { _bright = value; OnPropertyChanged("Bright"); }
        }
        public string RotationPeriod
        {
            get { return _rotationPeriod; }
            set { _rotationPeriod = value; OnPropertyChanged("RotationPeriod"); }
        }
        public string OrbitalPeriod
        {
            get { return _orbitalPeriod; }
            set { _orbitalPeriod = value; OnPropertyChanged("OrbitalPeriod"); }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
        }

        public static ObservableCollection<Planet> Planets { get; set; } = GetListOfPlanets();

        public static ObservableCollection<Planet> GetListOfPlanets()
        {
            return new ObservableCollection<Planet>()
            {
                new Planet() { Name = "Mercury", Structure = PlanetStructure.Rock, Bright=true, Radius = 2400, RotationPeriod = "59 days", OrbitalPeriod = "3 months", ImagePath = "ms-appx:/Other/Planets/Mercury.png" },
                new Planet() { Name = "Venus", Structure = PlanetStructure.Rock, Bright=true, Radius = 6100, RotationPeriod = "243 days", OrbitalPeriod = "7 months", ImagePath = "ms-appx:/Other/Planets/Venus.png" },
                new Planet() { Name = "Earth", Structure = PlanetStructure.Rock, Bright=true, Radius = 6400, RotationPeriod = "1 day", OrbitalPeriod = "1 year", ImagePath = "ms-appx:/Other/Planets/Earth.png" },
                new Planet() { Name = "Mars", Structure = PlanetStructure.Rock, Bright=true, Radius = 3400, RotationPeriod = "1 day, 37 min", OrbitalPeriod = "2 years", ImagePath = "ms-appx:/Other/Planets/Mars.png" },
                new Planet() { Name = "Jupiter", Structure = PlanetStructure.Gas, Bright=true, Radius = 71500, RotationPeriod = "1 day, 10 hrs", OrbitalPeriod = "12 years", ImagePath = "ms-appx:/Other/Planets/Jupiter.png" },
                new Planet() { Name = "Saturn", Structure = PlanetStructure.Gas, Bright=true, Radius = 60300, RotationPeriod = "1 day, 11 hrs", OrbitalPeriod = "30 years", ImagePath = "ms-appx:/Other/Planets/Saturn.png" },
                new Planet() { Name = "Uranus", Structure = PlanetStructure.Gas, Bright=false, Radius = 25600, RotationPeriod = "1 day, 17 hrs", OrbitalPeriod = "84 years", ImagePath = "ms-appx:/Other/Planets/Uranus.png" },
                new Planet() { Name = "Neptune", Structure = PlanetStructure.Gas, Bright=false, Radius = 24800, RotationPeriod = "1 day, 16 hrs", OrbitalPeriod = "165 years", ImagePath = "ms-appx:/Other/Planets/Neptune.png" },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
