using Diploma.DbStuff.Repositories;
using Diploma.Models;
using Diploma.Services.Interfaces;

namespace Diploma.Services.BusinessServices
{
    public class FriendsBusinessService : IService
    {
        private readonly FriendRepository _friendRepository;
        private readonly FriendBuilder _friendBuilder;

        public const decimal NUMBER_OF_FRIENDS_ON_PAGE = 30;

        public FriendsBusinessService(FriendRepository friendRepository, FriendBuilder friendBuilder)
        {
            _friendRepository = friendRepository;
            _friendBuilder = friendBuilder;
        }

        public PaginatorViewModel<FriendViewModel> GetUserFriendsOnPage(int userId, int page)
        {
            var data = _friendRepository.GetFriendsByUserIdOnPage(userId, page, (int)NUMBER_OF_FRIENDS_ON_PAGE);
            var paginatorOptions = new PaginatorOptionsViewModel();
            var paginatorViewModel = new PaginatorViewModel<FriendViewModel>();

            var pagesCount = (int)Math.Ceiling((decimal)data.FriendsCount / NUMBER_OF_FRIENDS_ON_PAGE);
            paginatorOptions.CurrentPage = pagesCount;
            paginatorOptions.AvailablePages = Enumerable.Range(1, pagesCount).ToList();
            paginatorViewModel.Items = data
                                    .Friends
                                    .Select(friend => _friendBuilder.RebuildFriendToFriendViewModel(friend))
                                    .ToList();
            paginatorViewModel.Options = paginatorOptions;
            return paginatorViewModel;
        }
    }
}
