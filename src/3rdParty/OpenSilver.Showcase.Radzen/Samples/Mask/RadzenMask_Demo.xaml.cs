using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

public partial class RadzenMask_Demo : UserControl
{
    public RadzenMask_Demo()
    {
        InitializeComponent();
        DataContext = new PhoneData();
    }

    public class PhoneData : INotifyPropertyChanged
    {
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
