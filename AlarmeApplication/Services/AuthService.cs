using AlarmeApplication.Models;

namespace AlarmeApplication.Services
{
    public class AuthService : IAuthService
    {
        private List<UserModel> _users = new List<UserModel>
        {
            new UserModel { UserId = 1, UserName = "operador01", Password = "pass123", Role = "operador"},
            new UserModel { UserId = 2, UserName = "operador02", Password = "pass123", Role = "operador"},
            new UserModel { UserId = 3, UserName = "oficial01", Password = "pass123", Role = "oficial"},
            new UserModel { UserId = 4, UserName = "oficial02", Password = "pass123", Role = "oficial"},
            new UserModel { UserId = 5, UserName = "usuario01", Password = "pass123", Role = "usuario"},
            new UserModel { UserId = 6, UserName = "usuario02", Password = "pass123", Role = "usuario"},
            new UserModel { UserId = 7, UserName = "supervisor", Password = "pass123", Role = "supervisor"},
        };

        public UserModel Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}
