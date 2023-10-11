using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Banquet_Hall_App.Models
{
    public class Order : ObservableObject
    {
        private uint _id;
        private DateTime _date;
        private Client _client;
        private uint _clientId;
        private decimal _totalCost;


        public virtual Client Client 
        { 
            get => _client;
            set 
            { 
                _client = value;
                OnPropertyChanged();
            } 
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
        public DateTime Date 
        { 
            get => _date; 
            set 
            { 
                _date = value; 
                OnPropertyChanged();
            } 
        }

        public uint ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalCost
        {
            get => _totalCost;
            set
            {
                _totalCost = value;
                OnPropertyChanged();
            }
        }
    }
}
