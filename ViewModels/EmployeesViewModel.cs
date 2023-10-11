using Banquet_Hall_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Banquet_Hall_App.BanquetSystemDbContext;

namespace Banquet_Hall_App.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees 
        {
            get => _employees; 
        }

        public Employee EmployeeInfo { get; set; } = new Employee();

        public Employee SelectedEmployee { get; set; } = new Employee();

        public EmployeesViewModel(MyDbContext context) : base(context)
        {
            this.context.Employee.Load();

            _employees = new ObservableCollection<Employee>(this.context.Employee.Local);
            this.context.Employee.Local.CollectionChanged += Employees_CollectionChanged;
        }

        #region Commands

        private RelayCommand _addEmployeeCommand;
        private RelayCommand _updateEmployeeCommand;
        private RelayCommand _deleteEmployeeCommand;

        public ICommand AddEmployeeCommand =>
            _addEmployeeCommand ??= new RelayCommand(
                () =>
                {
                    if (EmployeeInfo.Name == null || EmployeeInfo.PhoneNumber == null)
                    {
                        MessageBox.Show("Incorrect input data");
                        return;
                    }
                    context.Employee.Add(new Employee()
                    {
                        Name = EmployeeInfo.Name,
                        PhoneNumber = EmployeeInfo.PhoneNumber
                    });
                    context.SaveChanges();
                    MessageBox.Show("Added successfully");
                },
                () => EmployeeInfo != null);

        public ICommand UpdateEmployeeCommand =>
            _updateEmployeeCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedEmployee == null || SelectedEmployee.Name == null 
                    || SelectedEmployee.PhoneNumber == null)
                    {
                        MessageBox.Show("Incorrect item selection");
                        return;
                    }
                    context.Entry(SelectedEmployee).State = EntityState.Modified;
                    context.SaveChanges();
                },
                () => SelectedEmployee != null);

        public ICommand DeleteEmployeeCommand =>
            _deleteEmployeeCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedEmployee == null
                    || SelectedEmployee.Name == null
                    || SelectedEmployee.PhoneNumber == null)
                    {
                        MessageBox.Show("Incorrect item selection");
                        return;
                    }
                    context.Employee.Remove(SelectedEmployee);
                    context.SaveChanges();
                },
                () => SelectedEmployee != null);

        #endregion

        private void Employees_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    Employees.Add((Employee)newItem);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems)
                {
                    Employees.Remove((Employee)oldItem);
                }
            }
        }
    }
}
