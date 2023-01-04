using static MoS.Services.AuthenticationService.HashService;

namespace MoS.Implementations.AuthenticationImplementations
{
    public class HashImplementation : IHashService
    {
        public string Hash(string input)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(input);

            return passwordHash;
        }

        public bool Verify(string hashPassword, string password)
        {
            bool verified = BCrypt.Net.BCrypt.Verify(password, hashPassword);

            return verified;
        }
    }
}
