namespace DesiKhataApp.Services;

using System.Collections.ObjectModel;
using DesiKhataApp.Models;

public class CustomerEntryService
{
    // Singleton pattern
    private static readonly CustomerEntryService _instance = new();
    public static CustomerEntryService Instance => _instance;

    // Collection of entries
    public ObservableCollection<CustomerEntry> Entries { get; private set; }

    private CustomerEntryService()
    {
        Entries = new ObservableCollection<CustomerEntry>();
        LoadMockData();
    }

    // Get entries for a specific customer
    public ObservableCollection<CustomerEntry> GetEntriesForCustomer(string customerId)
    {
        var customerEntries = new ObservableCollection<CustomerEntry>();
        foreach (var entry in Entries)
        {
            if (entry.CustomerId == customerId)
            {
                customerEntries.Add(entry);
            }
        }
        return customerEntries;
    }

    // Add a new entry
    public void AddEntry(CustomerEntry entry)
    {
        // Calculate the new balance
        var customerEntries = GetEntriesForCustomer(entry.CustomerId).OrderBy(e => e.Date).ToList();
        decimal balance = 0;

        if (customerEntries.Count > 0)
        {
            balance = customerEntries.Last().Balance;
        }

        // Update the balance based on this new entry (YouGot - YouGave)
        balance += (entry.YouGot - entry.YouGave);
        entry.Balance = balance;

        Entries.Add(entry);

        // Update customer's totals
        CustomerService.Instance.UpdateCustomerBalances(
            entry.CustomerId,
            entry.YouGave,
            entry.YouGot
        );

        SaveEntries();
    }

    // Remove an entry
    public void RemoveEntry(string id)
    {
        var entry = Entries.FirstOrDefault(e => e.Id == id);
        if (entry != null)
        {
            Entries.Remove(entry);
            // Would need to recalculate all balances and update customer
            SaveEntries();
        }
    }

    // Get an entry by ID
    public CustomerEntry? GetEntryById(string id)
    {
        return Entries.FirstOrDefault(e => e.Id == id);
    }

    // Save entries to persistent storage (will implement with actual storage later)
    private void SaveEntries()
    {
        // In a real implementation, save to database or file
        // For now, we'll just keep it in memory
    }

    // Load mock data for testing
    private void LoadMockData()
    {
        if (Entries.Count == 0)
        {
            var customer = CustomerService.Instance.Customers.FirstOrDefault();
            if (customer != null)
            {
                // Add entries based on the screenshot
                var business = BusinessService.Instance.GetBusinessById(customer.BusinessId);
                if (business != null)
                {
                    // Sample data based on screenshot
                    var entry1 = new CustomerEntry
                    {
                        CustomerId = customer.Id,
                        BusinessId = business.Id,
                        Date = new DateTime(2025, 5, 7, 0, 17, 0),
                        Description =
                            "Dear KAPEEL, your FBL Card has been charged for PKR 9000 on 07-05-2025 00:15:52 at BAR B Q TONIGHT REST KARACHI PK.",
                        YouGot = 9000,
                        Balance = 9000,
                    };
                    Entries.Add(entry1);

                    var entry2 = new CustomerEntry
                    {
                        CustomerId = customer.Id,
                        BusinessId = business.Id,
                        Date = new DateTime(2025, 5, 5, 16, 11, 0),
                        Description =
                            "Dear KAPEEL, your FBL Card has been charged for PKR 725 on 05-05-2025 23:05:18 at BAR B Q TONIGHT REST KARACHI PK.",
                        YouGot = 725,
                        Balance = entry1.Balance + 725,
                    };
                    Entries.Add(entry2);

                    var entry3 = new CustomerEntry
                    {
                        CustomerId = customer.Id,
                        BusinessId = business.Id,
                        Date = new DateTime(2025, 5, 4, 16, 11, 0),
                        Description =
                            "Dear KAPEEL, your FBL Card has been charged for PKR 4131 on 04-05-2025 21:21:07 at COLETTE",
                        YouGot = 4131,
                        Balance = entry1.Balance + entry2.Balance + 4131,
                    };
                    Entries.Add(entry3);
                }
            }
        }
    }
}
