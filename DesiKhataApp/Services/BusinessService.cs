namespace DesiKhataApp.Services;

using System.Collections.ObjectModel;
using DesiKhataApp.Models;

public class BusinessService
{
    // Singleton pattern for the business service
    private static readonly BusinessService _instance = new();
    public static BusinessService Instance => _instance;

    // Collection of businesses
    public ObservableCollection<Business> Businesses { get; private set; }

    private BusinessService()
    {
        Businesses = new ObservableCollection<Business>();
        LoadMockData();
    }

    // Add a new business
    public void AddBusiness(Business business)
    {
        Businesses.Add(business);
        SaveBusinesses();
    }

    // Remove a business
    public void RemoveBusiness(string id)
    {
        var business = Businesses.FirstOrDefault(b => b.Id == id);
        if (business != null)
        {
            Businesses.Remove(business);
            SaveBusinesses();
        }
    }

    // Update a business
    public void UpdateBusiness(Business updatedBusiness)
    {
        var index = Businesses.ToList().FindIndex(b => b.Id == updatedBusiness.Id);
        if (index >= 0)
        {
            Businesses[index] = updatedBusiness;
            SaveBusinesses();
        }
    }

    // Get a business by ID
    public Business? GetBusinessById(string id)
    {
        return Businesses.FirstOrDefault(b => b.Id == id);
    }

    // Save businesses to persistent storage (will implement with actual storage later)
    private void SaveBusinesses()
    {
        // In a real implementation, save to database or file
        // For now, we'll just keep it in memory
    }

    // Load businesses from storage (mock data for now)
    private void LoadMockData()
    {
        // Add some mock data for testing
        if (Businesses.Count == 0)
        {
            Businesses.Add(
                new Business { Name = "Sharma General Store", PhoneNumber = "9876543210" }
            );

            Businesses.Add(new Business { Name = "Patel Electronics", PhoneNumber = "8765432109" });
        }
    }
}
