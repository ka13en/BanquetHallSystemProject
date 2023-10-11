using Banquet_Hall_App.BanquetSystemDbContext;

namespace Banquet_Hall_App.ViewModels
{
    public class ViewModelBase
    {
        protected readonly MyDbContext context;

        protected ViewModelBase(MyDbContext context)
        {
            this.context = context;
        }
    }
}
