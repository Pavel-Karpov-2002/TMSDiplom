$(document).ready(() => {
    const addFriend = $('.btn-friend');
    
    addFriend.click(() => {
        let divFriend = addFriend.parent();
        let userId = divFriend.find('.userId').val();
        let mainUserId = divFriend.find('.mainUserId').val();
        const friend = {
            userId: parseInt(userId),
            mainUserId: parseInt(mainUserId)
        }
        const isAdd = divFriend.hasClass('add-friend');
        let url = '';
        if (isAdd) {
            url = '/FriendApi/AddFriend';
        }
        else {
            url = '/FriendApi/DeleteFriend';
        }
        $.get(url, friend, function (response) {
                alert(response);
                if (isAdd) {
                    addFriend.val('Удалить из друзей');
                    divFriend.addClass('remove-friend');
                    divFriend.removeClass('add-friend');
                }
                else {
                    addFriend.val('Добавить в друзья');
                    divFriend.addClass('add-friend');
                    divFriend.removeClass('remove-friend');
                }
            });
    });
});