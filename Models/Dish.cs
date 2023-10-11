using CommunityToolkit.Mvvm.ComponentModel;

namespace Banquet_Hall_App.Models
{
    public class Dish : ObservableObject
    {
        private uint _id;
        private string _name;
        private string _description;
        private uint _price;

        public uint Id 
        { 
            get => _id; 
            set 
            { 
                _id = value;
                OnPropertyChanged();
            } 
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

        public string Description 
        { 
            get => _description; 
            set 
            { 
                _description = value;
                OnPropertyChanged();
            }  
        }

        public uint Price
        {
            get
            {
                if (_price == null)
                {
                    return 0;
                }
                else return (uint)_price;
            }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
    }
}
