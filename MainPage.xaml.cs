using MauiApp3.Models;
using MauiApp3.Service;
using MauiApp3.View;
using MP3Player;
using System.Text.Json;

namespace MauiApp3
{
    public partial class MainPage : ContentPage
    {
        private LocalService localService;
        public MainPage(LocalService localService)
        {
            InitializeComponent();
            this.localService = localService;
        }
        public async void OnRegister(object sender, EventArgs e)
        {
            if (NameLabel == null || NameLabel.Text.Length <1 || PasswardLabel == null || PasswardLabel.Text.Length < 1)
            {
                successful.IsVisible = false;
                fail.IsVisible = true;
            }
            var result = localService.Register(NameLabel.Text, PasswardLabel.Text);
            if(result)
            {
                successful.IsVisible = true;
                fail.IsVisible = false;
            }
            else
            {
                successful.IsVisible = false;
                fail.IsVisible = true;
            }
        }
        public async void OnLogin(object sender, EventArgs e)
        {
            var result = localService.Login(NameLabel.Text, PasswardLabel.Text);
            if(result)
            {
                successful.IsVisible = true;
                fail.IsVisible = false;
                await Shell.Current.GoToAsync($"{nameof(HomePage)}", false);
            }
            else
            {
                successful.IsVisible = false;
                fail.IsVisible = true;
            }
        }

    }
}