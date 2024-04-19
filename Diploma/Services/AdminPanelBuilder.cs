using Diploma.DbStuff.Models;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services
{
    public class AdminPanelBuilder : IService
    {
        private readonly UserBuilder _userBuilder;

        public AdminPanelBuilder(UserBuilder userBuilder)
        {
            _userBuilder = userBuilder;
        }

        public AdminPanelViewModel BuildAdminPanelViewModel(List<UserProfile> users)
        {
            var usersViewModel = users.Select(user => _userBuilder.BuildBlockProfileViewModel(user)).ToList();

            return new AdminPanelViewModel
            {
                Users = usersViewModel
            };
        }
    }
}
