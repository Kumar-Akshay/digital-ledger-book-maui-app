namespace DesiKhataApp.Pages;

public partial class SignupPage : ContentPage
{
    public SignupPage()
    {
        InitializeComponent();
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        // Validate signup form
        string name = NameEntry.Text?.Trim() ?? string.Empty;
        string email = EmailEntry.Text?.Trim() ?? string.Empty;
        string password = PasswordEntry.Text?.Trim() ?? string.Empty;
        string confirmPassword = ConfirmPasswordEntry.Text?.Trim() ?? string.Empty;

        if (
            string.IsNullOrEmpty(name)
            || string.IsNullOrEmpty(email)
            || string.IsNullOrEmpty(password)
            || string.IsNullOrEmpty(confirmPassword)
        )
        {
            await DisplayAlert("Error", "Please fill all fields", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords don't match", "OK");
            return;
        }

        // TODO: Add actual registration logic here

        // Navigate to the Business List page after successful signup
        await Shell.Current.GoToAsync("//BusinessListPage");
    }

    private async void OnLoginTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
