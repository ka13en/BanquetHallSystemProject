using Banquet_Hall_App.BanquetSystemDbContext;
using Banquet_Hall_App.ViewModels;
using System.Windows;

namespace Banquet_Hall_App.Views
{
    public partial class NewClientView : Window
    {
        public NewClientView(MyDbContext context)
        {
            InitializeComponent();

            DataContext = new ClientsViewModel(context);
        }
    }
}
