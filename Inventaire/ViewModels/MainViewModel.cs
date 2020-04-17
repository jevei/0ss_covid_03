using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private BaseViewModel _vm;

		public BaseViewModel VM
		{
			get { return _vm; }
			set {
				_vm = value;
				OnPropertyChanged();
			}
		}

		CustomerViewModel customerViewModel;
		InvoiceViewModel invoiceViewModel;

		public ChangeViewCommand ChangeViewCommand { get; set; }
		public RelayCommand NewCustomerCommand { get; private set; }
		public RelayCommand NewInvoiceCommand { get; private set; }
		public RelayCommand DisplayInvoiceCommand { get; private set; }
		public RelayCommand DisplayCustomerCommand { get; private set; }

		public MainViewModel()
		{
			ChangeViewCommand = new ChangeViewCommand(ChangeView);
			NewCustomerCommand = new RelayCommand(NewCustomer);
			customerViewModel = new CustomerViewModel();
			invoiceViewModel = new InvoiceViewModel(customerViewModel.Customers);
			NewInvoiceCommand = new RelayCommand(NewInvoice,CanExecuteNewInvoice);
			DisplayInvoiceCommand = new RelayCommand(DisplayInvoice);
			DisplayCustomerCommand = new RelayCommand(DisplayCustomer, CanExecuteDisplayCustomer);
			VM = customerViewModel;

		}

		private void ChangeView(string vm)
		{
			switch (vm)
			{
				case "customers":
					VM = customerViewModel;
					break;
				case "invoices":
					VM = invoiceViewModel;
					break;
			}
		}
		private void NewCustomer(object c)
		{
			Customer customer = new Customer();
			customerViewModel.Customers.Add(customer);
			customerViewModel.SelectedCustomer = customer;
			VM = customerViewModel;
		}
		private void DisplayInvoice(object i)
		{
			Invoice invoice = (Invoice)i;
			invoiceViewModel.SelectedInvoice = invoice;
			VM = invoiceViewModel;
		}
		private void NewInvoice(object c)
		{
			var customer = (Customer)c;
			var invoice = new Invoice(customer);
			customer.Invoices.Add(invoice);
			DisplayInvoice(invoice);
		}
		private bool CanExecuteNewInvoice(object c)
		{
			return c == null ? false : true;
		}
		private void DisplayCustomer(object c)
		{
			Customer customer = (Customer)c;
			customerViewModel.SelectedCustomer = customer;
			VM = customerViewModel;
		}
		private bool CanExecuteDisplayCustomer(object c)
		{
			return c == null ? false : true;
		}
	}
}
