using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.Services
{
    /// <summary>
    /// Опции генерации токена для авторизации
    /// </summary>
    public class AuthOptions
    {
        public const string ISSUER = "Smartcom"; 
        public const string AUDIENCE = "Smartcom";
        const string KEY = "qwerty1234567890"; 
        public const int LIFETIME = 9999; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
