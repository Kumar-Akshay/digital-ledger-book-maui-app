namespace DesiKhataApp;

using DesiKhataApp.Pages;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent(); // Register routes for navigation - using absolute route paths
        Routing.RegisterRoute("//SignupPage", typeof(SignupPage));
        Routing.RegisterRoute("//LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("//BusinessListPage", typeof(BusinessListPage));
        Routing.RegisterRoute("//BusinessDetailsPage", typeof(BusinessDetailsPage));
        Routing.RegisterRoute("CustomerListPage", typeof(CustomerListPage));
        Routing.RegisterRoute("CustomerDetailsPage", typeof(CustomerDetailsPage));
    }
}
