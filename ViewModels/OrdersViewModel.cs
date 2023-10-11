using Banquet_Hall_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;
using Banquet_Hall_App.Miscellaneous;
using System;
using Banquet_Hall_App.BanquetSystemDbContext;
using CommunityToolkit.Mvvm.Input;

namespace Banquet_Hall_App.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
        }

        public Order OrderInfo { get; set; } = new Order();

        public Order SelectedOrder { get; set; } = new Order();

        public OrdersViewModel(MyDbContext context) : base(context)
        {
            this.context.Orders.Load();

            this.context.Orders.Local.CollectionChanged += Orders_CollectionChanged;

            _orders = new ObservableCollection<Order>(this.context.Orders.Local);
        }

        #region Commands

        private RelayCommand _addOrderCommand;
        private RelayCommand _updateOrderCommand;
        private RelayCommand _deleteOrderCommand;

        public ICommand AddOrderCommand =>
            _addOrderCommand ??= new RelayCommand(
                () =>
                {
                    if (OrderInfo.Client == null 
                    || OrderInfo.Date.Date <= DateTime.Today.Date)
                    {
                        MessageBox.Show("Incorrect input data");
                        return;
                    }
                    context.Orders.Add(new Order()
                    {
                        Date = OrderInfo.Date,
                        Client = OrderInfo.Client,
                        ClientId = OrderInfo.Client.Id,
                        TotalCost = Calc.OrderStartPrice
                    });
                    context.SaveChanges();
                    MessageBox.Show("Added successfully");
                });

        public ICommand UpdateOrderCommand =>
            _updateOrderCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedOrder.Client == null)
                    {
                        MessageBox.Show("Incorrect order selection");
                        return;
                    }
                    context.Entry(SelectedOrder).State = EntityState.Modified;
                    context.SaveChanges();
                });

        public ICommand DeleteOrderCommand =>
            _deleteOrderCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedOrder.Client == null)
                    {
                        MessageBox.Show("Incorrect order selection");
                        return;
                    }
                    context.Orders.Remove(SelectedOrder);
                    context.SaveChanges();
                });

        #endregion

        private void Orders_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newOrder in e.NewItems)
                {
                    Orders.Add((Order)newOrder);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldOrder in e.OldItems)
                {
                    Orders.Remove((Order)oldOrder);
                }
            }
        }
    }
}
