using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Transcriber.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<TranscriptionRequest> TranscriptionRequests { get; set; } = new List<TranscriptionRequest>();

    }

}

