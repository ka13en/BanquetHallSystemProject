using Banquet_Hall_App.BanquetSystemDbContext;
using Banquet_Hall_App.ViewModels;
using System.Windows;

namespace Banquet_Hall_App.Views
{
    public partial class NewDishView : Window
    {
        public NewDishView(MyDbContext context)
        {
            InitializeComponent();

            DataContext = new DishesViewModel(context);
        }
    }
}
