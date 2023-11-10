using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace OpenSilver.Samples.Showcase
{
    public class League : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private ObservableCollection<Division> _divisions;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ObservableCollection<Division> Divisions
        {
            get { return _divisions; }
            set { _divisions = value; }
        }

        void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
            ObservableCollection<string> strings = new ObservableCollection<string>() { "azsa", "aszae", "zefzed", "swdxcfsd" };
        }

        public static ObservableCollection<League> Leagues { get; set; } = GetListOfLeagues();

        public static ObservableCollection<League> GetListOfLeagues()
        {
            return new ObservableCollection<League>()
            {
                new League() { Name = "League A", Divisions = new ObservableCollection<Division> ()
                {
                    new Division()
                    {
                        Name = "Division A",
                        Teams = new ObservableCollection<Team> ()
                        {
                            new Team() { Name = "Team 1" },
                            new Team() { Name = "Team 2" },
                            new Team() { Name = "Team 3" },
                            new Team() { Name = "Team 4" }
                        }
                    },
                    new Division()
                    {
                        Name = "Division B",
                        Teams = new ObservableCollection<Team> ()
                        {
                            new Team() { Name = "Team 5" },
                            new Team() { Name = "Team 6" },
                            new Team() { Name = "Team 7" },
                            new Team() { Name = "Team 8" }
                        }
                    },
                    new Division()
                    {
                        Name = "Division C",
                        Teams = new ObservableCollection<Team> ()
                        {
                            new Team() { Name = "Team 9" },
                            new Team() { Name = "Team 10" },
                            new Team() { Name = "Team 11" },
                            new Team() { Name = "Team 12" }
                        }
                    }
                }
                },
                new League() { Name = "League B", Divisions = new ObservableCollection<Division> ()
                {
                    new Division()
                    {
                        Name = "Division A",
                        Teams = new ObservableCollection<Team> ()
                        {
                            new Team() { Name = "Team 13" },
                            new Team() { Name = "Team 14" },
                            new Team() { Name = "Team 15" },
                            new Team() { Name = "Team 16" }
                        }
                    },
                    new Division()
                    {
                        Name = "Division B",
                        Teams = new ObservableCollection<Team> ()
                        {
                            new Team() { Name = "Team 17" },
                            new Team() { Name = "Team 18" },
                            new Team() { Name = "Team 19" },
                            new Team() { Name = "Team 20" }
                        }
                    },
                    new Division()
                    {
                        Name = "Division C",
                        Teams = new ObservableCollection<Team> ()
                        {
                            new Team() { Name = "Team 21" },
                            new Team() { Name = "Team 22" },
                            new Team() { Name = "Team 23" },
                            new Team() { Name = "Team 24" }
                        }
                    }
                }
                },

            };
        }
    }

    public class Division
    {
        private string _name;
        private ObservableCollection<Team> _teams;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ObservableCollection<Team> Teams
        {
            get { return _teams; }
            set { _teams = value; }
        }
    }

    public class Team
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
