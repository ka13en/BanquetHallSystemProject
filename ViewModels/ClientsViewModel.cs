using Banquet_Hall_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Banquet_Hall_App.BanquetSystemDbContext;

namespace Banquet_Hall_App.ViewModels
{
    public class ClientsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Client> _clients;

        public ObservableCollection<Client> Clients 
        {
            get => _clients;
        }

        public Client ClientInfo { get; set; } = new Client();

        public Client SelectedClient { get; set; } = new Client();

        public ClientsViewModel(MyDbContext context) : base(context)
        {
            this.context.Client.Load();

            this.context.Client.Local.CollectionChanged += Clients_CollectionChanged;
            _clients = new ObservableCollection<Client>(this.context.Client.Local);
        }

        #region Commands

        private RelayCommand _addClientCommand;
        private RelayCommand _updateClientCommand;
        private RelayCommand _deleteClientCommand;

        public ICommand AddClientCommand =>
            _addClientCommand ??= new RelayCommand(
                () =>
                {
                    if (ClientInfo.Name == null || ClientInfo.PhoneNumber == null)
                    {
                        MessageBox.Show("Incorrect input data");
                        return;
                    }
                    context.Client.Add(new Client()
                    {
                        Name = ClientInfo.Name,
                        PhoneNumber = ClientInfo.PhoneNumber
                    });
                    context.SaveChanges();
                    MessageBox.Show("Added successfully");
                },
                () => ClientInfo != null);

        public ICommand UpdateClientCommand =>
            _updateClientCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedClient == null || SelectedClient.Name == null || SelectedClient.PhoneNumber == null)
                    {
                        MessageBox.Show("Incorrect client selection");
                        return;
                    }
                    context.Entry(SelectedClient).State = EntityState.Modified;
                    context.SaveChanges();
                },
                () => SelectedClient != null);

        public ICommand DeleteClientCommand =>
            _deleteClientCommand ??= new RelayCommand(
                () =>
                {
                    if (SelectedClient == null || SelectedClient.Name == null || SelectedClient.PhoneNumber == null)
                    {
                        MessageBox.Show("Incorrect client selection");
                        return;
                    }
                    context.Client.Remove(SelectedClient);
                    context.SaveChanges();
                },
                () => SelectedClient != null);

        #endregion

        private void Clients_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    Clients.Add((Client)newItem);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems)
                {
                    Clients.Remove((Client)oldItem);
                }
            }
        }
    }
}
