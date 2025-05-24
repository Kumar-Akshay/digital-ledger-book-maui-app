namespace DesiKhataApp.Services;

using System.Collections.ObjectModel;
using DesiKhataApp.Models;

public class CustomerService
{
    // Singleton pattern
    private static readonly CustomerService _instance = new();
    public static CustomerService Instance => _instance;

    // Collection of customers
    public ObservableCollection<Customer> Customers { get; private set; }

    private CustomerService()
    {
        Customers = new ObservableCollection<Customer>();
        LoadMockData();
    }

    // Get customers for a specific business
    public ObservableCollection<Customer> GetCustomersForBusiness(string businessId)
    {
        var businessCustomers = new ObservableCollection<Customer>();
        foreach (var customer in Customers)
        {
            if (customer.BusinessId == businessId)
            {
                businessCustomers.Add(customer);
            }
        }
        return businessCustomers;
    }

    // Add a new customer
    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
        SaveCustomers();
    }

    // Remove a customer
    public void RemoveCustomer(string id)
    {
        var customer = Customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            Customers.Remove(customer);
            SaveCustomers();
        }
    }

    // Update a customer
    public void UpdateCustomer(Customer updatedCustomer)
    {
        var index = Customers.ToList().FindIndex(c => c.Id == updatedCustomer.Id);
        if (index >= 0)
        {
            Customers[index] = updatedCustomer;
            SaveCustomers();
        }
    }

    // Get a customer by ID
    public Customer? GetCustomerById(string id)
    {
        return Customers.FirstOrDefault(c => c.Id == id);
    }

    // Update customer balances based on entries
    public void UpdateCustomerBalances(string customerId, decimal gaveAmount, decimal gotAmount)
    {
        var customer = GetCustomerById(customerId);
        if (customer != null)
        {
            customer.TotalGave += gaveAmount;
            customer.TotalGot += gotAmount;
            UpdateCustomer(customer);
        }
    }

    // Save customers to persistent storage (will implement with actual storage later)
    private void SaveCustomers()
    {
        // In a real implementation, save to database or file
        // For now, we'll just keep it in memory
    }

    // Load mock data for testing
    private void LoadMockData()
    {
        // Add some mock customers for testing
        if (Customers.Count == 0)
        {
            var business = BusinessService.Instance.Businesses.FirstOrDefault();
            if (business != null)
            {
                Customers.Add(
                    new Customer
                    {
                        Name = "Kapeel",
                        PhoneNumber = "9876543210",
                        BusinessId = business.Id,
                        TotalGave = 0,
                        TotalGot = 13856,
                    }
                );

                Customers.Add(
                    new Customer
                    {
                        Name = "Raj Kumar",
                        PhoneNumber = "8765432109",
                        BusinessId = business.Id,
                        TotalGave = 5000,
                        TotalGot = 2500,
                    }
                );
            }
        }
    }
}
