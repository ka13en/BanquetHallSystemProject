using Banquet_Hall_App.BanquetSystemDbContext;
using Banquet_Hall_App.ViewModels;
using System.Windows;

namespace Banquet_Hall_App.Views
{
    public partial class AddDishesToOrders : Window
    {
        public AddDishesToOrders(MyDbContext context)
        {
            InitializeComponent();

            var ordersVM = new OrdersViewModel(context);
            var orderDishVM = new OrderDishViewModel(context);
            var dishesVM = new DishesViewModel(context);

            OrdersComboBox.DataContext = orderDishVM;
            OrdersComboBox.ItemsSource = ordersVM.Orders;

            DishesComboBox.DataContext = orderDishVM;
            DishesComboBox.ItemsSource = dishesVM.Dishes;

            OrdersComboBox.SelectedItem = orderDishVM.ItemInfo.Order;
            DishesComboBox.SelectedItem = orderDishVM.ItemInfo.Dish;

            DishAmountTextBox.DataContext = orderDishVM;
            AddItemButton.DataContext = orderDishVM;
        }
    }
}
