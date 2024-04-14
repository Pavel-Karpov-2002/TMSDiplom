$(document).ready(() => {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7015/posts")
        .build();

    const addPostButton = $('.btn-add-post');

    addPostButton.click(() => {
        const descriptionPost = $('.btn-post-description');
        if (descriptionPost.val().length) {
            let userId = $('.user-id').val();
            let description = descriptionPost.val()
            let creatorUserName = $('.user-name').val();
            let userAvatarUrl = $('.user-avatar-url').val();
            let time = Date.now;
            const post = {
                CreatorUserId: parseInt(userId),
                CreatorUserName: creatorUserName,
                CreatorAvatarUrl: userAvatarUrl,
                Description: description,
                DateOfCreation: time
            }
            addPostOnServer(post);
        }
    });

    hub.on('LastUserPosts', (lastPosts) => {
        lastPosts.forEach((post) => {
            addPostOnPage(post.creatorUserId, post.creatorUserName, post.creatorAvatarUrl, post.description, post.dateOfCreation);
        });
    });

    hub.on('UserGotNewPost', (post) => {
        addPostOnPage(post.creatorUserId, post.creatorUserName, post.creatorAvatarUrl, post.description, post.dateOfCreation);
    });

    hub.start().then(() => {
        let userId = $('.user-id').val();
        hub.invoke('GetLastUserPosts', parseInt(userId));
    });
   
    const addPostOnServer = (post) => {
        hub.invoke('AddNewPost', {...post});
    };

    const addPostOnPage = (userId, userName, userAvatarUrl, description, dateOfCreation) => {
        const newPostBlock = $('.post.post-template').clone();
        newPostBlock.removeClass('post-template');
        newPostBlock.find('.post-creator').text(userName);
        newPostBlock.find('.user-src-avatarUrl').attr('src', userAvatarUrl);
        newPostBlock.find('.post-description').text(description);
        var timestamp = new Date(dateOfCreation).getTime();
        var datetime = new Date(timestamp);
        newPostBlock.find('.comment-time-of-writing').text(datetime.toLocaleString());

        $('.posts').prepend(newPostBlock);
    }
});