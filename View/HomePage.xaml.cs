using MauiApp3.Models;
using MauiApp3.Service;
using Microsoft.Maui.Storage;
using Plugin.LocalNotification;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Text.Json;


namespace MauiApp3.View;

public partial class HomePage : ContentPage
{
   // private LocalNotificationCenter _notificationCenter;
    private IAudioManager audioManager;
    private int _currentlyPlayingFilePath = 0;
    private AudioFile _currentlyPlayingFile = null;
    private IAudioPlayer _player;
    private List<AudioFile> audioFiles;
    private bool pause=false;
    public ObservableCollection<AudioFile> MyItems { get; set; }
    public HomePage(IAudioManager audioManager)
	{
		InitializeComponent();
        MyItems = new ObservableCollection<AudioFile>();
        this.audioManager = audioManager;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        //StopAudio();
    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        LoadData();

    }
    private async void LoadData()
    {
        // When the data is loaded, update the collection
        if(audioFiles == null)
        {
            var jsonFile = await FileSystem.OpenAppPackageFileAsync("audioData.json");
            using var streamReader = new StreamReader(jsonFile);
            var json = await streamReader.ReadToEndAsync();
            audioFiles = JsonSerializer.Deserialize<List<AudioFile>>(json);
        }
        
        MyListView.ItemsSource = audioFiles;
    }
    public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        StopAudio();
        AudioFile item = args.SelectedItem as AudioFile;
        PlayName(item);
    }
    public async void PlayName(AudioFile file)
    {
        var steamRaw = await FileSystem.OpenAppPackageFileAsync($"{file.AudioName}.mp3");
        _player = audioManager.CreatePlayer(steamRaw);
        _currentlyPlayingFilePath = file.Id;
        _currentlyPlayingFile = file;
        PlayAudio();
    }
    public void StopAudio()
    {
        if (_currentlyPlayingFilePath != 0)
        {
            _player.Stop();
            _currentlyPlayingFilePath = 0;
            _currentlyPlayingFile = null;
        }
    }
    public void PauseAudio()
    {
        if (_currentlyPlayingFilePath != 0)
        {
            _player.Pause();
        }
    }
    public void PlayAudio()
    {
        if (_currentlyPlayingFilePath != 0)
        {
            _player.Play();
            Notification(_currentlyPlayingFile.AudioName, _currentlyPlayingFile.AudioDescription);
            
        }
    }
    private void Next_Button_Clicked(object sender, EventArgs e)
    {
        if(_currentlyPlayingFilePath!=0)
        {

            _currentlyPlayingFilePath = _currentlyPlayingFilePath + 1 > 8 ? 1 : _currentlyPlayingFilePath + 1;
            MyListView.SelectedItem = audioFiles[_currentlyPlayingFilePath-1];
        }
    }
    private void Previous_Button_Clicked(object sender, EventArgs e)
    {
        if (_currentlyPlayingFilePath != 0)
        {
            _currentlyPlayingFilePath = _currentlyPlayingFilePath - 1 < 1 ? 8 : _currentlyPlayingFilePath - 1;
            MyListView.SelectedItem = audioFiles[_currentlyPlayingFilePath - 1];
        }
    }
    private void Pause_play_Button_Clicked(object sender, EventArgs e)
    {
        if (_currentlyPlayingFilePath != 0 && pause == false)
        {
            PauseAudio();
            pause = true;
        }
        else if(_currentlyPlayingFilePath != 0 && pause == true)
        {
            PlayAudio();
            pause = false;
        }
    }
    public void Notification(string title, string description)
    {
        long[] pattern = { 0, 500, 250, 500 };
        var request = new NotificationRequest()
        {
            NotificationId = 0,
            Title = title,
            Subtitle = title,
            Description = description,
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateTime.Now
            },
            Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions()
            {
                VibrationPattern = pattern
            }

        };
        LocalNotificationCenter.Current.Show(request);
    }


}