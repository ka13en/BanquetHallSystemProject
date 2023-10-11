using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banquet_Hall_App.Models
{
    public class OrderDish : ObservableObject
    {
        private Dish _dish;
        private Order _order;
        private uint _orderId;
        private uint _dishId;
        private uint _dishesAmount;

        public virtual Dish Dish 
        { 
            get => _dish; 
            set 
            {
                _dish = value;
                OnPropertyChanged();
            } 
        }

        public virtual Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        [Key]
        [Column(Order = 1)]
        public uint OrderId
        {
            get => _orderId;
            set
            {
                _orderId = value;
                OnPropertyChanged();
            }
        }

        [Key]
        [Column(Order = 2)]
        public uint DishId 
        {
            get => _dishId; 
            set { 
                _dishId = value;
                OnPropertyChanged();
            } 
        }

        public uint DishesAmount 
        { 
            get => _dishesAmount; 
            set 
            { 
                _dishesAmount = value;
                OnPropertyChanged();
            } 
        }
    }
}
