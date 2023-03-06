using Auth.Models;

namespace Auth.Storage
{
    public interface IJwtManager
    {
        Tokens Authenticate(User user);
    }
}
