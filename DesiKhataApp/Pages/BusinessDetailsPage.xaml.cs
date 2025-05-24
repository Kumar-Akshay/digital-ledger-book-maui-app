namespace DesiKhataApp.Pages;

using DesiKhataApp.Models;
using DesiKhataApp.Services;

[QueryProperty(nameof(BusinessId), "BusinessId")]
public partial class BusinessDetailsPage : ContentPage
{
    private string _businessId = string.Empty;
    private Business? _currentBusiness;

    public string BusinessId
    {
        get => _businessId;
        set
        {
            _businessId = value;
            LoadBusiness();
        }
    }

    public BusinessDetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Refresh business data
        LoadBusiness();
    }

    private void LoadBusiness()
    {
        if (string.IsNullOrEmpty(_businessId))
            return;

        _currentBusiness = BusinessService.Instance.GetBusinessById(_businessId);
        if (_currentBusiness != null)
        {
            // Update UI with business info
            BusinessNameLabel.Text = _currentBusiness.Name;
            BusinessPhoneLabel.Text = _currentBusiness.PhoneNumber;
            Title = _currentBusiness.Name;
        }
    }

    private async void OnBackToListClicked(object sender, EventArgs e)
    {
        // Navigate back to business list
        await Shell.Current.GoToAsync("//BusinessListPage");
    }

    private async void OnManageCustomersClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_businessId))
            return;

        // Navigate to customer list for this business
        var navigationParameter = new Dictionary<string, object> { { "BusinessId", _businessId } };

        await Shell.Current.GoToAsync("CustomerListPage", navigationParameter);
    }
}
