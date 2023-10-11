namespace Banquet_Hall_App.Models
{
    public class Employee : Person
    {
        private uint _id;

        public Employee() { }

        public Employee(uint id, string fullname, string phoneNumber) : base(fullname, phoneNumber)
        {
            _id = id;
        }

        public uint Id 
        { 
            get => _id; 
            set 
            { 
                _id = value;
                OnPropertyChanged();
            } 
        }
    }
}
