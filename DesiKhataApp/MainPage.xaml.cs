namespace DesiKhataApp;

using DesiKhataApp.Pages;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // This can be used to check if we should automatically navigate to Business page
        // For example, if the user is already logged in
    }

    private async void OnBusinessClicked(object sender, EventArgs e)
    {
        await NavigateToBusinessPage();
    }

    public async Task NavigateToBusinessPage()
    {
        await Shell.Current.GoToAsync("//BusinessListPage");
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private async void OnLogoutClicked(object? sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (answer)
        {
            // TODO: Clear any user session data here

            // Navigate back to login page
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
