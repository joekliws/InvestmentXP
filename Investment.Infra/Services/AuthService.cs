using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Infra.Services
{
    public interface IAuthService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        void ReadPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        void Login(string userLogin, string password);
        void Logout();

        string GenerateToken();

    }
    public class AuthService
    {
    }
}
