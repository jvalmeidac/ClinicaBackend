using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend.API.Security
{
    public class SigningConfigurations
    {
        private const string SECRET_KEY = "2168ddb67aa96688d0bcfaeee3ab3573";
        public SigningCredentials SigningCredentials { get; }
        private readonly SymmetricSecurityKey _signingKey = new(Encoding.ASCII.GetBytes(SECRET_KEY));

        public SigningConfigurations()
        {
            SigningCredentials = new(_signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
