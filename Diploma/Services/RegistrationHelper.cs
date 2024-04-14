using Diploma.DbStuff.Repositories;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class RegistrationHelper : IService
    {
        private readonly UserRepository _userRepository;

        public RegistrationHelper(UserRepository db)
        {
            _userRepository = db;
        }

        public bool IsLoginExists(string login)
        {
            return _userRepository.AnyUserWithLogin(login);
        }

        public bool IsEmailExists(string email)
        {
            return _userRepository.AnyUserWithEmail(email);
        }
    }
}
