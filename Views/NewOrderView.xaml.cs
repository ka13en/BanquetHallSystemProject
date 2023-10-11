using Banquet_Hall_App.BanquetSystemDbContext;
using Banquet_Hall_App.ViewModels;
using System.Windows;

namespace Banquet_Hall_App.Views
{
    public partial class NewOrderView : Window
    {
        private MyDbContext _context { get; }

        public NewOrderView(MyDbContext context)
        {
            InitializeComponent();

            _context = context;
            
            OrdersViewModel ordersVM = new(_context);

            ClientsComboBox.DataContext = ordersVM;
            ClientsComboBox.ItemsSource = new ClientsViewModel(_context).Clients;
            ClientsComboBox.SelectedItem = ordersVM.OrderInfo.Client;
            OrderDatePicker.DataContext = ordersVM;
            EnterButton.DataContext = ordersVM;
        }

        private void NewClientButton_Click(object sender, RoutedEventArgs e)
        {
            new NewClientView(_context).Show();
        }
    }
}
