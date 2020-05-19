using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace BillingManagement.UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private BaseViewModel _vm;

		ObservableCollection<Customer> dbCustomers;
		BillingManagementContext db = new BillingManagementContext();
		ObservableCollection<Invoice> dbInvoices;

		public BaseViewModel VM
		{
			get { return _vm; }
			set {
				_vm = value;
				OnPropertyChanged();
			}
		}

		private string searchCriteria;

		public string SearchCriteria
		{
			get { return searchCriteria; }
			set { 
				searchCriteria = value;
				OnPropertyChanged();
			}
		}


		CustomerViewModel customerViewModel;
		InvoiceViewModel invoiceViewModel;

		public ChangeViewCommand ChangeViewCommand { get; set; }

		public DelegateCommand<object> AddNewItemCommand { get; private set; }

		public DelegateCommand<Invoice> DisplayInvoiceCommand { get; private set; }
		public DelegateCommand<Customer> DisplayCustomerCommand { get; private set; }

		public DelegateCommand<Customer> AddInvoiceToCustomerCommand { get; private set; }

		public RelayCommand<Customer> SearchCommand { get; private set; }


		public MainViewModel()
		{
			dbInvoices = new ObservableCollection<Invoice>();
			dbCustomers = new ObservableCollection<Customer>();

			var sort = dbCustomers.OrderBy(x => x.LastName);
			var CustomersSorted = new ObservableCollection<Customer>(sort);

			ChangeViewCommand = new ChangeViewCommand(ChangeView);
			DisplayInvoiceCommand = new DelegateCommand<Invoice>(DisplayInvoice);
			DisplayCustomerCommand = new DelegateCommand<Customer>(DisplayCustomer);

			AddNewItemCommand = new DelegateCommand<object>(AddNewItem, CanAddNewItem);
			AddInvoiceToCustomerCommand = new DelegateCommand<Customer>(AddInvoiceToCustomer);
			SearchCommand = new RelayCommand<Customer>(SearchCustomer, CanAddNewItem);

			customerViewModel = new CustomerViewModel();
			invoiceViewModel = new InvoiceViewModel(customerViewModel.Customers);

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

		private void DisplayInvoice(Invoice invoice)
		{
			invoiceViewModel.SelectedInvoice = invoice;
			VM = invoiceViewModel;
		}

		private void DisplayCustomer(Customer customer)
		{
			customerViewModel.SelectedCustomer = customer;
			VM = customerViewModel;
		}

		private void AddInvoiceToCustomer(Customer c)
		{
			var invoice = new Invoice(c);
			c.Invoices.Add(invoice);
			DisplayInvoice(invoice);
		}

		private void AddNewItem (object item)
		{
			if (VM == customerViewModel)
			{
				var c = new Customer();
				customerViewModel.Customers.Add(c);
				customerViewModel.SelectedCustomer = c;
			}
		}

		private bool CanAddNewItem(object o)
		{
			bool result = false;

			result = VM == customerViewModel;
			return result;
		}

		private void SearchCustomer(object parametre)
		{
			string user_input = searchCriteria as string;

			Customer SelectedCustomer = customerViewModel.SelectedCustomer;
			IEnumerable<Customer> customers = customerViewModel.Customers.ToList<Customer>();
			IEnumerable<Customer> FoundCustomer = customerViewModel.Customers.ToList<Customer>();

			FoundCustomer = customers.Where(c => c.Name.ToUpper().StartsWith(user_input.ToUpper()) || c.LastName.ToUpper().StartsWith(user_input.ToUpper()));

			if (FoundCustomer.Count() < 0)
				MessageBox.Show("Aucun " + user_input + " trouvé");
			else
				customerViewModel.SelectedCustomer = FoundCustomer.First();
		}

	}
}
