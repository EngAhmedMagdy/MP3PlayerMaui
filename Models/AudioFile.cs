namespace MauiApp3.Models
{
    public class AudioFile
    {
        public int Id { get; set; }
        public string AudioName { get; set; }
        public string AudioDescription { get; set; }
        public byte[] AudioData { get; set; }
        //public virtual ICollection<UserAudio>? UserAudios { get; set; }
    }

}
