using System;
using System.ComponentModel;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHTML5.Wrappers.DevExtreme.DataGrid.Examples
{
	public class Employee : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		// This method is called by the Set accessor of each property.
		// The CallerMemberName attribute that is applied to the optional propertyName
		// parameter causes the property name of the caller to be substituted as an argument.
		private void NotifyPropertyChanged(String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private int _ID;
		public int ID
		{
			get
			{
				return _ID;
			}
			set
			{
				if (value != _ID)
				{
					_ID = value;
					NotifyPropertyChanged("ID");
				}
			}
		}

		private string _firstName;
		public string firstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				if (value != _firstName)
				{
					_firstName = value;
					NotifyPropertyChanged("firstName");
				}
			}
		}

		private string _lastName;
		public string lastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				if (value != _lastName)
				{
					_lastName = value;
					NotifyPropertyChanged("lastName");
				}
			}
		}

		private DateTime _hireDate;
		public DateTime hireDate
		{
			get
			{
				return _hireDate;
			}
			set
			{
				if (value != _hireDate)
				{
					_hireDate = value;
					NotifyPropertyChanged("hireDate");
				}
			}
		}

		public Employee()
		{
			ID = 0;
			firstName = "";
			lastName = "";
			hireDate = new DateTime();
		}

		public Employee(int id, string FirstName, string LastName, DateTime HireDate)
		{
			ID = id;
			firstName = FirstName;
			lastName = LastName;
			hireDate = HireDate;
		}
	}
}
