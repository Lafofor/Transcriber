using Transcriber.Models;

namespace Transcriber.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);

    }
}
