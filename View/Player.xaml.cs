using Plugin.Maui.Audio;

namespace MauiApp3.View;

public partial class Player : ContentPage
{
    private IAudioManager audioManager;
    public Player(IAudioManager audioManager)
	{
		InitializeComponent();
        this.audioManager = audioManager;

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        byte[] binaryData = null;
        using (var stream = new MemoryStream(binaryData))
        {
            using (var reader = new BinaryReader(stream))
            {
                // Read the audio data from the binary stream
                byte[] audioData = reader.ReadBytes((int)stream.Length);

                // Convert the audio data to a memory stream
                using (var audioStream = new MemoryStream(audioData))
                {
                    // Use the audio stream as needed, for example:
                    var player = audioManager.CreatePlayer(audioStream);
                    player.Play();
                }
            }
        }
        
    }
}