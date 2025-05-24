namespace DesiKhataApp.Pages;

using System.Collections.ObjectModel;
using System.Windows.Input;
using DesiKhataApp.Models;
using DesiKhataApp.Services;

public partial class BusinessListPage : ContentPage
{
    // ViewModel properties
    public ObservableCollection<Business> Businesses { get; set; }
    public ICommand RefreshCommand { get; set; }

    public BusinessListPage()
    {
        InitializeComponent();

        // Initialize ViewModel properties
        Businesses = BusinessService.Instance.Businesses;
        RefreshCommand = new Command(RefreshBusinesses);

        // Set binding context
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshBusinesses();
    }

    private void RefreshBusinesses()
    {
        // This refreshes the UI with the latest data
        BusinessesRefreshView.IsRefreshing = true;

        // In a real app, you might fetch from database or API here

        BusinessesRefreshView.IsRefreshing = false;
    }

    private async void OnAddBusinessClicked(object sender, EventArgs e)
    {
        // Show dialog to add a new business
        string name = await DisplayPromptAsync(
            "New Business",
            "Enter business name:",
            placeholder: "Business name"
        );

        if (string.IsNullOrWhiteSpace(name))
            return;

        string phone = await DisplayPromptAsync(
            "New Business",
            "Enter phone number:",
            placeholder: "Phone number"
        );

        if (!string.IsNullOrWhiteSpace(name))
        {
            var newBusiness = new Business { Name = name, PhoneNumber = phone ?? string.Empty };

            BusinessService.Instance.AddBusiness(newBusiness);
        }
    }

    private async void OnBusinessSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Business selectedBusiness)
        {
            // Clear selection
            BusinessesCollection.SelectedItem = null;

            // Navigate to business details
            var navigationParameter = new Dictionary<string, object>
            {
                { "BusinessId", selectedBusiness.Id },
            };

            await Shell.Current.GoToAsync($"//BusinessDetailsPage", navigationParameter);
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (answer)
        {
            // Navigate back to login page
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
