namespace Banquet_Hall_App.Models
{
    public class Client : Person
    {
        private uint _id;

        public Client(string name, string phoneNumber, uint id) : base(name, phoneNumber)
        {
            _id = id;
        }

        public Client() {}

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
