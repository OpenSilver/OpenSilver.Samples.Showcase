using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OpenSilver.Samples.Showcase
{
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
        [Display(Name = "Age", Description = "Age as positive number")]
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

        private string _email;
        [RegularExpression(@"^([a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]\.?)*[a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]@((([a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_]\.?)*[a-zA-Z0-9\!\%\$\%\*\/\?\|\^\{\}\`\~\&\'\+\-\=_])|(\[\d+\.\d+\.\d+\.\d+\]))$", ErrorMessage = "Not a valid e-mail address.")]
        [Display(Name = "Email", Description = "An e-mail address of the form <name>@<domain>, such as john@johndoe.com")]
        [Required]
        public string Email
        {
            get => _email;
            set
            {
                if (value != _email)
                {
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Email" });
                    _email = value;
                    RaisePropertyChanged(nameof(Email));
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
