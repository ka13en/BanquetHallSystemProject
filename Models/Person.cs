using CommunityToolkit.Mvvm.ComponentModel;

namespace Banquet_Hall_App.Models
{
    public abstract class Person : ObservableObject
    {
        private string _name;
        private string _phoneNumber;

        protected Person() {}

        protected Person(string name, string phoneNumber)
        {
            _name = name;
            _phoneNumber = phoneNumber;
        }

        public string Name 
        { 
            get => _name; 
            set 
            { 
                _name = value;
                OnPropertyChanged();
            } 
        }

        public string PhoneNumber 
        { 
            get => _phoneNumber; 
            set 
            {
                _phoneNumber = value;
                OnPropertyChanged();
            } 
        }
    }
}
