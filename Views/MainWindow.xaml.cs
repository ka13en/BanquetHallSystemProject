using Banquet_Hall_App.BanquetSystemDbContext;
using Banquet_Hall_App.ViewModels;
using System.Windows;

namespace Banquet_Hall_App.Views
{
    public partial class MainWindow : Window
    {
        private readonly MyDbContext _сontext;

        public MainWindow()
        {
            InitializeComponent();

            _сontext = new MyDbContext();

            OrdersTab.DataContext = new OrdersViewModel(_сontext);

            ClientsTab.DataContext = new ClientsViewModel(_сontext);
            
            DishesTab.DataContext = new DishesViewModel(_сontext);
            
            EmployeesTab.DataContext = new EmployeesViewModel(_сontext);

        }

        private void ButtonAddNewOrder_Click(object sender, RoutedEventArgs e)
        {
            new NewOrderView(_сontext).Show();
        }

        private void ButtonOpenDishCreatorWindow_Click(object sender, RoutedEventArgs e)
        {
            new NewDishView(_сontext).Show();
        }

        private void ButtonAddNewClient_Click(object sender, RoutedEventArgs e)
        {
            new NewClientView(_сontext).Show();
        }

        private void ButtonOpenOrderDishWindow_Click(object sender, RoutedEventArgs e)
        {
            new OrderDishesView(_сontext).Show();
        }

        private void ButtonAddDishToOrder_Click(object sender, RoutedEventArgs e)
        {
            new AddDishesToOrders(_сontext).Show();
        }

        private void ButtonAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            new NewEmployeeView(_сontext).Show();
        }        
        
        private void Info_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This application's purpose is to help administrator" +
                " to manage organization. You can add, edit and delete all available" +
                " entities (clients, Orders, dishes, employees)");
        }

        private void MenuItemHelpTool_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("From the main window you can control clients, " +
                "orders, dishes and employees. Buttons on the left give you opportunity" +
                " to get access to dishes that was ordered for each order (button \"Orders " +
                "details\") and also add dishes for order (button \"Add Dish to order\")");
        }
    }
}
