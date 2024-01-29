namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.ComponentModel
open System.Runtime.CompilerServices
open System.Text

type Team() =
    let mutable _name = ""
    member val Name = _name with get, set

type Division() =
    let mutable _name = ""
    member val Name = _name with get, set
    member val Teams = new ObservableCollection<Team>() with get, set

type League() = 
    let mutable _name = ""
    let mutable _divisions = new ObservableCollection<Division>()
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()

    member val Name = _name with get, set
    member val Divisions = _divisions with get, set

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

    member private this.OnPropertyChanged(caller : string) =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(caller))

    static member Leagues = League.GetListOfLeagues()

    static member GetListOfLeagues() =
        ObservableCollection<League>(
            [
                new League(Name = "League A", Divisions = ObservableCollection<Division>([
                    new Division(
                        Name = "Division A",
                        Teams = new ObservableCollection<Team>([
                            new Team(Name = "Team 1")
                            new Team(Name = "Team 2")
                            new Team(Name = "Team 3")
                            new Team(Name = "Team 4")
                        ]))
                    new Division(
                        Name = "Division B",
                        Teams = new ObservableCollection<Team>([
                            new Team(Name = "Team 5")
                            new Team(Name = "Team 6")
                            new Team(Name = "Team 7")
                            new Team(Name = "Team 8")
                        ]))
                    new Division(
                        Name = "Division C",
                        Teams = new ObservableCollection<Team>([
                            new Team(Name = "Team 9")
                            new Team(Name = "Team 10")
                            new Team(Name = "Team 11")
                            new Team(Name = "Team 12")
                        ]))
                    ]))
                new League(Name = "League B", Divisions = ObservableCollection<Division>([
                    new Division(
                        Name = "Division A",
                        Teams = new ObservableCollection<Team>([
                            new Team(Name = "Team 13")
                            new Team(Name = "Team 14")
                            new Team(Name = "Team 15")
                            new Team(Name = "Team 16")
                        ]))
                    new Division(
                        Name = "Division B",
                        Teams = new ObservableCollection<Team>([
                            new Team(Name = "Team 17")
                            new Team(Name = "Team 18")
                            new Team(Name = "Team 19")
                            new Team(Name = "Team 20")
                        ]))
                    new Division(
                        Name = "Division C",
                        Teams = new ObservableCollection<Team>([
                            new Team(Name = "Team 21")
                            new Team(Name = "Team 22")
                            new Team(Name = "Team 23")
                            new Team(Name = "Team 24")
                        ]))
                    ]))
            ])
