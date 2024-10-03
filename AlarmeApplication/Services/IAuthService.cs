using AlarmeApplication.Models;

namespace AlarmeApplication.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(string  username, string password);
    }
}
