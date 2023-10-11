using Banquet_Hall_App.BanquetSystemDbContext;
using Banquet_Hall_App.Miscellaneous;
using Banquet_Hall_App.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Banquet_Hall_App.ViewModels
{
    public class OrderDishViewModel : ViewModelBase
    {
        private readonly ObservableCollection<OrderDish> _orderDishes;

        public ObservableCollection<OrderDish> OrderDishes
        {
            get => _orderDishes;
        }

        public OrderDish ItemInfo { get; set; } = new OrderDish();

        public OrderDish SelectedItem { get; set; } = new OrderDish();

        public OrderDishViewModel(MyDbContext context) : base(context)
        {
            this.context.OrderDish.Load();

            this.context.OrderDish.Local.CollectionChanged += OrderDishes_CollectionChanged;
            _orderDishes = new ObservableCollection<OrderDish>(this.context.OrderDish.Local);
        }

        #region Commands

        private RelayCommand _addItemCommand;
        private RelayCommand _updateItemCommand;
        private RelayCommand _deleteItemCommand;

        private OrderDish? FindItemIfAlreadyPresent()
        {
            return context.OrderDish.SingleOrDefault(
                od => od.OrderId == ItemInfo.Order.Id
                && od.DishId == ItemInfo.Dish.Id);
        }

        public ICommand AddItemCommand =>
            _addItemCommand ??= new RelayCommand(
                () =>
                {
                    if (ItemInfo.Dish == null || ItemInfo.Order == null || ItemInfo.DishesAmount <= 0)
                    {
                        MessageBox.Show("Incorrect input data");
                        return;
                    }
                    var existingOrderDish = FindItemIfAlreadyPresent();
                    if (existingOrderDish != null) existingOrderDish.DishesAmount += ItemInfo.DishesAmount;
                    else
                    {
                        context.OrderDish.Add(new OrderDish()
                        {
                            Order = ItemInfo.Order,
                            OrderId = ItemInfo.Order.Id,
                            Dish = ItemInfo.Dish,
                            DishId = ItemInfo.Dish.Id,
                            DishesAmount = ItemInfo.DishesAmount
                        });
                    }
                    context.SaveChanges();
                    MessageBox.Show("Added successfully");
                },
                () => ItemInfo != null);

        public ICommand UpdateItemCommand =>
            _updateItemCommand ??= new RelayCommand(
                () => 
                {
                    if (SelectedItem.Dish == null || SelectedItem.Order == null)
                    {
                        MessageBox.Show("Incorrect item selection");
                        return;
                    }
                    context.Entry(SelectedItem).State = EntityState.Modified;
                    context.SaveChanges();
                    Calc.UpdateOrderTotalCost(context, SelectedItem.OrderId);
                },
                () => SelectedItem != null);

        public ICommand DeleteItemCommand =>
            _deleteItemCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedItem.Dish == null || SelectedItem.Order == null)
                    {
                        MessageBox.Show("Incorrect item selection");
                        return;
                    }
                    context.OrderDish.Remove(SelectedItem);
                    context.SaveChanges();
                },
                () => SelectedItem != null);

        #endregion

        private void OrderDishes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    var newOrderDishItem = (OrderDish)newItem;
                    OrderDishes.Add((OrderDish)newItem);
                    Calc.UpdateOrderTotalCost(context, newOrderDishItem.OrderId);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems)
                {
                    var oldOrderDishItem = (OrderDish)oldItem;
                    OrderDishes.Remove((OrderDish)oldItem);
                    Calc.UpdateOrderTotalCost(context, oldOrderDishItem.OrderId);
                }
            }
        }
    }
}
