using MauiApp3.Models;
using MauiApp3.Service;

namespace MauiApp3.View;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    public async void OnLogin(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(HomePage)}", false);

    }
}