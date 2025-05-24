namespace DesiKhataApp.Pages;

using System.Collections.ObjectModel;
using System.Windows.Input;
using DesiKhataApp.Models;
using DesiKhataApp.Services;

[QueryProperty(nameof(BusinessId), "BusinessId")]
public partial class CustomerListPage : ContentPage
{
    // ViewModel properties
    public ObservableCollection<Customer> Customers { get; set; }
    public ICommand RefreshCommand { get; set; }

    private string _businessId = string.Empty;
    private Business? _currentBusiness;

    public string BusinessId
    {
        get => _businessId;
        set
        {
            _businessId = value;
            LoadBusiness();
            LoadCustomers();
        }
    }

    public CustomerListPage()
    {
        InitializeComponent();

        // Initialize commands
        RefreshCommand = new Command(RefreshCustomers);

        // Initialize collections
        Customers = new ObservableCollection<Customer>();

        // Set binding context
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshCustomers();
    }

    private void LoadBusiness()
    {
        if (string.IsNullOrEmpty(_businessId))
            return;

        _currentBusiness = BusinessService.Instance.GetBusinessById(_businessId);
        if (_currentBusiness != null)
        {
            BusinessNameLabel.Text = _currentBusiness.Name;
        }
    }

    private void LoadCustomers()
    {
        if (string.IsNullOrEmpty(_businessId))
            return;

        Customers.Clear();
        var businessCustomers = CustomerService.Instance.GetCustomersForBusiness(_businessId);

        foreach (var customer in businessCustomers)
        {
            Customers.Add(customer);
        }
    }

    private void RefreshCustomers()
    {
        CustomersRefreshView.IsRefreshing = true;

        LoadCustomers();

        CustomersRefreshView.IsRefreshing = false;
    }

    private async void OnAddCustomerClicked(object sender, EventArgs e)
    {
        // Show dialog to add a new customer
        string name = await DisplayPromptAsync(
            "New Customer",
            "Enter customer name:",
            placeholder: "Customer name"
        );

        if (string.IsNullOrWhiteSpace(name))
            return;

        string phone = await DisplayPromptAsync(
            "New Customer",
            "Enter phone number:",
            placeholder: "Phone number"
        );

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrEmpty(_businessId))
        {
            var newCustomer = new Customer
            {
                Name = name,
                PhoneNumber = phone ?? string.Empty,
                BusinessId = _businessId,
            };

            CustomerService.Instance.AddCustomer(newCustomer);
            RefreshCustomers();
        }
    }

    private async void OnCustomerSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Customer selectedCustomer)
        {
            // Clear selection
            CustomersCollection.SelectedItem = null;

            // Navigate to customer details
            var navigationParameter = new Dictionary<string, object>
            {
                { "CustomerId", selectedCustomer.Id },
                { "BusinessId", _businessId },
            };

            await Shell.Current.GoToAsync("CustomerDetailsPage", navigationParameter);
        }
    }

    private async void OnBackToBusinessClicked(object sender, EventArgs e)
    {
        // Navigate back to business details
        await Shell.Current.GoToAsync($"//BusinessDetailsPage?BusinessId={_businessId}");
    }
}
