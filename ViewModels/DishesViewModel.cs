using Banquet_Hall_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Banquet_Hall_App.Miscellaneous;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using Banquet_Hall_App.BanquetSystemDbContext;

namespace Banquet_Hall_App.ViewModels
{
    public class DishesViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Dish> _dishes;

        public ObservableCollection<Dish> Dishes 
        {
            get => _dishes;
        }

        public Dish DishInfo { get; set; } = new Dish();

        public Dish SelectedDish { get; set; } = new Dish();

        public DishesViewModel(MyDbContext context) : base(context)
        {
            this.context.Dish.Load();

            this.context.Dish.Local.CollectionChanged += Dishes_CollectionChanged;

            _dishes = new ObservableCollection<Dish>(this.context.Dish.Local);
        }

        #region Commands

        private RelayCommand _addDishCommand;
        private RelayCommand _updateDishCommand;
        private RelayCommand _deleteDishCommand;

        public ICommand AddDishCommand =>
            _addDishCommand ??= new RelayCommand(
                () =>
                {
                    if(DishInfo.Name == null)
                    {
                        MessageBox.Show("Incorrect input data");
                        return;
                    }
                    var dish = new Dish()
                    {
                        Name = DishInfo.Name,
                        Price = DishInfo.Price
                    };
                    if (DishInfo.Description != null)
                    {
                        dish.Description = DishInfo.Description;
                    }
                    context.Dish.Add(dish);
                    context.SaveChanges();
                },
                () => DishInfo != null);

        public ICommand DeleteDishCommand =>
            _deleteDishCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedDish == null || SelectedDish.Name == null)
                    {
                        MessageBox.Show("Incorrect dish selection");
                        return;
                    }
                    context.Dish.Remove(SelectedDish);
                    context.SaveChanges();
                },
                () => SelectedDish != null);

        public ICommand UpdateDishCommand =>
            _updateDishCommand ??= new RelayCommand(
                () =>
                {
                    if(SelectedDish == null || SelectedDish.Name == null)
                    {
                        MessageBox.Show("Incorrect dish selection");
                        return;
                    }
                    context.Entry(SelectedDish).State = EntityState.Modified;
                    Calc.UpdateAllOrdersTotalCost(context);
                    context.SaveChanges();
                },
                () => SelectedDish != null);

        #endregion

        private void Dishes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    Dishes.Add((Dish)newItem);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems)
                {
                    Dishes.Remove((Dish)oldItem);
                }
            }
        }

    }
}
