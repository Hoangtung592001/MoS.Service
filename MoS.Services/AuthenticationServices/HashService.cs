namespace MoS.Services.AuthenticationService
{
    public class HashService
    {
        private readonly IHashService _repository;

        public HashService(IHashService repository)
        {
            _repository = repository;
        }

        public interface IHashService
        {
            string Hash(string input);
            bool Verify(string hashPassword, string password);
        }

        public string Hash(string input)
        {
            return _repository.Hash(input);
        }

        public bool Verify(string hashPassword, string password)
        {
            return _repository.Verify(hashPassword, password);
        }
    }
}
