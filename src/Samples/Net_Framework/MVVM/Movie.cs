using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace OpenSilver.Samples.Showcase;

public partial class Movie : ObservableValidator
{
    [ObservableProperty]
    [Required(ErrorMessage = "Title is required.")]
    private string _title;

    [ObservableProperty]
    private MovieStatus _status;

    [ObservableProperty]
    private DateTime? _dateLastWatched;

    [ObservableProperty]
    private bool _favorite;

    [ObservableProperty]
    [Range(0.0, 1.0, ErrorMessage = "Rating must be between 0 and 1.")]
    private double _myRating;
}

public enum MovieStatus
{
    ToWatch,
    Watching,
    Watched,
    ToWatchAgain,
    Abandoned
}
