// filepath: /Users/akshaykumar/Desktop/Projects/DesiKhataApp/DesiKhataApp/App.xaml.cs
namespace DesiKhataApp;

using System;
using System.Threading.Tasks;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Create a window with AppShell
        var shell = new AppShell();
        var window = new Window(shell);

        // Schedule navigation to LoginPage after the shell is created and the window is ready
        Dispatcher.Dispatch(async () =>
        {
            try
            {
                await Task.Delay(100); // Small delay to ensure shell is initialized
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        });

        return window;
    }
}
