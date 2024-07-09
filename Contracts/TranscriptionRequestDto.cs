using System.ComponentModel.DataAnnotations;

namespace Transcriber.Contracts
{
    public class TranscriptionRequestDto
    {
        [Required(ErrorMessage = "File name is required")]
        public string? FileName { get; set; }

        [Required(ErrorMessage = "Body is required")]
        public string? Body { get; set; }
    }
}
