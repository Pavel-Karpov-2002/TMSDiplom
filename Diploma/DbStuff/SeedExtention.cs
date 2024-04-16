using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Services;

namespace Diploma.DbStuff
{
    public class SeedExtention
    {
        public static void Seed(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                SeedRole(serviceScope.ServiceProvider);
                SeedUser(serviceScope.ServiceProvider);
                SeedFriends(serviceScope.ServiceProvider);
            }
        }

        private static void SeedUser(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetService<UserRepository>();
            var userBuilder = serviceProvider.GetService<UserBuilder>();
            var userProfileBuilder = serviceProvider.GetService<UserProfileBuilder>();


            if (!userRepository.AnyUserWithLogin("admin"))
            {
                var birthday = new DateTime(2010, 10, 23);
                var admin = userBuilder.BuildUser("admin", "123", "test@test.com", "Admin Adminov", null);
                admin.UserProfile = userProfileBuilder.BuildUserProfile(admin, birthday);
                admin.UserProfile.Roles.Add(GetRole(serviceProvider, Roles.Admin.ToString()));
                userRepository.Add(admin);
            }

            if (!userRepository.AnyUserWithLogin("moderator"))
            {
                var birthday = new DateTime(2009, 11, 23);
                var moderator = userBuilder.BuildUser("moderator", "123", "test@test.com", "Moderator Moderatorov", null);
                moderator.UserProfile = userProfileBuilder.BuildUserProfile(moderator, birthday);
                moderator.UserProfile.Roles.Add(GetRole(serviceProvider, Roles.Moderator.ToString()));
                userRepository.Add(moderator);
            }

            if (!userRepository.AnyUserWithLogin("user"))
            {
                var birthday = new DateTime(2015, 8, 23);
                var user = userBuilder.BuildUser("user", "123", "test@test.com", "User Userov", null);
                user.UserProfile = userProfileBuilder.BuildUserProfile(user, birthday);
                user.UserProfile.Roles.Add(GetRole(serviceProvider, Roles.User.ToString()));
                userRepository.Add(user);
            }
        }

        private async static void SeedFriends(IServiceProvider serviceProvider)
        {
            var userProfileRepository = serviceProvider.GetService<UserProfileRepository>();
            var friendRepository = serviceProvider.GetService<FriendRepository>();

            if (!friendRepository.Any())
            {
                var admin = userProfileRepository.GetAllInformationAboutUserByLogin("admin");
                var moderator = userProfileRepository.GetAllInformationAboutUserByLogin("moderator");
                var user = userProfileRepository.GetAllInformationAboutUserByLogin("user");
                friendRepository.MutualAdditionToFriends(user, admin);
                friendRepository.MutualAdditionToFriends(user, moderator);
            }
        }

        private static void SeedRole(IServiceProvider serviceProvider)
        {
            var roleRepository = serviceProvider.GetService<RoleRepository>();
            if (roleRepository.Any() == false)
            {
                var role = new Role
                {
                    Name = Roles.Admin.ToString()
                };
                roleRepository.Add(role);

                role = new Role
                {
                    Name = Roles.Moderator.ToString()
                };
                roleRepository.Add(role);

                role = new Role
                {
                    Name = Roles.User.ToString()
                };
                roleRepository.Add(role);
            }
        }

        private static Role GetRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleRepository = serviceProvider.GetService<RoleRepository>();
            var role = roleRepository.GetRoleByName(roleName);
            return role;
        }
    }
}
