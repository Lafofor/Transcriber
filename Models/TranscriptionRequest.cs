using System.ComponentModel.DataAnnotations;

namespace Transcriber.Models
{
    public class TranscriptionRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? FileName { get; set; }

        public string? Body { get; set; }

        public string? TranscribedText { get; set; }

        public AppUser? User { get; set; }

    }
}
