using System;
using System.ComponentModel;

namespace OpenSilver.Samples.Showcase
{
    //Validation:
    public class Person : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name cannot be empty.");
                }
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private int _age = 1;
        public int Age
        {
            get { return _age; }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Age cannot be lower than 0.");
                }
                _age = value;
                RaisePropertyChanged("Age");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
