using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiApp3.Models
{
    public class UserAudio
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AudioFileId { get; set; }
    }
}
