namespace DesiKhataApp.Pages;

using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using DesiKhataApp.Models;
using DesiKhataApp.Services;

[QueryProperty(nameof(CustomerId), "CustomerId")]
[QueryProperty(nameof(BusinessId), "BusinessId")]
public partial class CustomerDetailsPage : ContentPage
{
    // ViewModel properties
    public ObservableCollection<CustomerEntry> Entries { get; set; }

    private string _customerId = string.Empty;
    private string _businessId = string.Empty;
    private Customer? _currentCustomer;
    private Business? _currentBusiness;

    public string CustomerId
    {
        get => _customerId;
        set
        {
            _customerId = value;
            LoadCustomer();
            LoadEntries();
        }
    }

    public string BusinessId
    {
        get => _businessId;
        set
        {
            _businessId = value;
            LoadBusiness();
        }
    }

    public CustomerDetailsPage()
    {
        InitializeComponent();

        // Initialize collections
        Entries = new ObservableCollection<CustomerEntry>();

        // Set binding context
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadEntries();
        UpdateUI();
    }

    private void LoadCustomer()
    {
        if (string.IsNullOrEmpty(_customerId))
            return;

        _currentCustomer = CustomerService.Instance.GetCustomerById(_customerId);
        if (_currentCustomer != null)
        {
            CustomerNameLabel.Text = _currentCustomer.Name;
            CustomerPhoneLabel.Text = _currentCustomer.PhoneNumber;
            Title = _currentCustomer.Name;
            UpdateUI();
        }
    }

    private void LoadBusiness()
    {
        if (string.IsNullOrEmpty(_businessId))
            return;

        _currentBusiness = BusinessService.Instance.GetBusinessById(_businessId);
    }

    private void LoadEntries()
    {
        if (string.IsNullOrEmpty(_customerId))
            return;

        Entries.Clear();
        var customerEntries = CustomerEntryService
            .Instance.GetEntriesForCustomer(_customerId)
            .OrderByDescending(e => e.Date)
            .ToList();

        foreach (var entry in customerEntries)
        {
            Entries.Add(entry);
        }
    }

    private void UpdateUI()
    {
        if (_currentCustomer == null)
            return;

        // Format currency amounts
        string currencySymbol = "₹";

        YouGaveTotalLabel.Text = string.Format(
            "{0}{1:N0}",
            currencySymbol,
            _currentCustomer.TotalGave
        );
        YouGotTotalLabel.Text = string.Format(
            "{0}{1:N0}",
            currencySymbol,
            _currentCustomer.TotalGot
        );
        BalanceLabel.Text = string.Format("{0}{1:N0}", currencySymbol, _currentCustomer.Balance);

        // Set balance color
        BalanceLabel.TextColor =
            _currentCustomer.Balance >= 0
                ? Color.FromArgb("#2e7d32") // Green for positive or zero
                : Color.FromArgb("#c62828"); // Red for negative
    }

    // Transaction data class to hold the dialog result
    private class TransactionData
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    // Simple method to show transaction dialog for both amount and description
    private async Task<TransactionData> ShowTransactionDialog(
        string transactionType,
        string colorHex
    )
    {
        // Step 1: Ask for amount
        string amountString = await DisplayPromptAsync(
            transactionType,
            "Enter amount:",
            "Next",
            "Cancel",
            "Amount",
            keyboard: Keyboard.Numeric,
            maxLength: 10
        );

        // Check if amount is valid
        if (string.IsNullOrWhiteSpace(amountString))
            return new TransactionData();

        if (!decimal.TryParse(amountString, out decimal amount))
            return new TransactionData();

        // Step 2: Ask for description (optional)
        string description = await DisplayPromptAsync(
            transactionType,
            "Enter description (optional):",
            "Add",
            "Skip",
            "Description",
            maxLength: 100
        );

        // Return the transaction data
        return new TransactionData { Amount = amount, Description = description ?? string.Empty };
    }

    // Button click handlers - these need to match exactly what's in the XAML
    private async void OnYouGaveClicked(object sender, EventArgs e)
    {
        if (_currentCustomer == null || _currentBusiness == null)
            return;

        // Show dialog to collect amount and description in one go
        var transactionData = await ShowTransactionDialog("YOU GAVE", "#c62828");

        if (transactionData.Amount <= 0)
            return;

        // Create new entry
        var entry = new CustomerEntry
        {
            CustomerId = _customerId,
            BusinessId = _businessId,
            Date = DateTime.Now,
            Description = string.IsNullOrWhiteSpace(transactionData.Description)
                ? $"You gave {transactionData.Amount:N0} to {_currentCustomer.Name}"
                : transactionData.Description,
            YouGave = transactionData.Amount,
            YouGot = 0,
        };

        // Add entry
        CustomerEntryService.Instance.AddEntry(entry);

        // Refresh data
        LoadCustomer();
        LoadEntries();
        UpdateUI();
    }

    private async void OnYouGotClicked(object sender, EventArgs e)
    {
        if (_currentCustomer == null || _currentBusiness == null)
            return;

        // Show dialog to collect amount and description in one go
        var transactionData = await ShowTransactionDialog("YOU GOT", "#2e7d32");

        if (transactionData.Amount <= 0)
            return;

        // Create new entry
        var entry = new CustomerEntry
        {
            CustomerId = _customerId,
            BusinessId = _businessId,
            Date = DateTime.Now,
            Description = string.IsNullOrWhiteSpace(transactionData.Description)
                ? $"You received {transactionData.Amount:N0} from {_currentCustomer.Name}"
                : transactionData.Description,
            YouGave = 0,
            YouGot = transactionData.Amount,
        };

        // Add entry
        CustomerEntryService.Instance.AddEntry(entry);

        // Refresh data
        LoadCustomer();
        LoadEntries();
        UpdateUI();
    }
}
