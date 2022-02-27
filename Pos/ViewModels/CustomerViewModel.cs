using Common;
using CommunityToolkit.Mvvm.Input;
using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pos.ViewModels
{
    public class CustomerViewModel : PropertyChangedBase
    {
        private IUnitOfWork _uow;

        public RelayCommand ClickMeCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand InsertCommand { get; private set; }

        public CustomerViewModel(IUnitOfWork uow)
        {
            _uow = uow;

            ClickMeCommand = new RelayCommand(OnClickMe);
            DeleteCommand = new RelayCommand(OnDelete);
            InsertCommand = new RelayCommand(OnInsert);
        }

        private void OnDelete()
        {
            DataCache.Instance.Customers.Delete(SelectedCustomer);

            Customers.Remove(SelectedCustomer);
        }

        private void OnInsert()
        {
            Customers.Add(DataCache.Instance.Customers.Add(new Customer()
            {
                FirstName = "TestingNewCustomer"
            }));
        }

        public void UpdateCustomers()
        {
            Customers = new(DataCache.Instance.Customers);
        }

        private void OnClickMe()
        {
            DataCache.Instance.Customers.SaveAsync().ConfigureAwait(false);
        }

        private ObservableCollection<Customer> _customers = new();
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }
    }
}
