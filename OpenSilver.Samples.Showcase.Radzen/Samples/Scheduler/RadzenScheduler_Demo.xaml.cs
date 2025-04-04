using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class RadzenScheduler_Demo : UserControl
    {
        public RadzenScheduler_Demo()
        {
            this.InitializeComponent();
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>()
            {
                new Appointment { Start = DateTime.Today.AddDays(-2), End = DateTime.Today.AddDays(-2), Text = "Birthday" },
                new Appointment { Start = DateTime.Today.AddDays(-11), End = DateTime.Today.AddDays(-10), Text = "Day off" },
                new Appointment { Start = DateTime.Today.AddDays(-10), End = DateTime.Today.AddDays(-8), Text = "Work from home" },
                new Appointment { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(12), Text = "Online meeting" },
                new Appointment { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(13), Text = "Skype call" },
                new Appointment { Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(14).AddMinutes(30), Text = "Dentist appointment" },
                new Appointment { Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(12), Text = "Vacation" },
            };
            this.DataContext = appointments;
        }
    }
}
