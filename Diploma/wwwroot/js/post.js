$(document).ready(() => {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7015/posts")
        .build();

    const addPostButton = $('.btn-add-post');
    let ip = "";

    $.getJSON("https://api.ipify.org?format=json",
        function (data) {
            ip = data.ip;
        });

    addPostButton.click(() => {
        const descriptionPost = $('.btn-post-description');
        if (descriptionPost.val().length) {
            let userId = $('.user-id').val();
            let description = descriptionPost.val()
            let creatorUserName = $('.user-name').val();
            let userAvatarUrl = $('.user-avatar-url').val();
            let time = Date.now();
            const post = {
                CreatorUserId: parseInt(userId),
                CreatorUserName: creatorUserName,
                CreatorAvatarUrl: userAvatarUrl,
                Description: description,
                DateOfCreation: new Date(time)
            }
            addPostOnServer(post);
            descriptionPost.val('');
        }
    });

    hub.on('LastUserPosts', (lastPosts) => {
        lastPosts.forEach((post) => {
            addPostOnPage(post.id, post.creatorUserName, post.creatorAvatarUrl, post.description, post.dateOfCreation, post.country + " " + post.city, post.countryCode);
        });
    });

    hub.on('UserGotNewPost', (post) => {
        addPostOnPage(post.id, post.creatorUserName, post.creatorAvatarUrl, post.description, post.dateOfCreation, post.country + " " + post.city, post.countryCode);
    });

    hub.start().then(() => {
        let userId = window.location.pathname.split('/').pop();
        hub.invoke('GetLastUserPosts', parseInt(userId));
    });
   
    const addPostOnServer = (post) => {
        const addPostModel = {
            post: post,
            ip: ip
        }
        hub.invoke('AddNewPost', addPostModel);
    };

    const addPostOnPage = (postId, userName, userAvatarUrl, description, dateOfCreation, location, locationCode) => {
        const newPostBlock = $('.post.post-template').clone();
        newPostBlock.removeClass('post-template');
        newPostBlock.find('.post-creator').text(userName);
        newPostBlock.find('.user-avatarUrl').attr('src', userAvatarUrl);
        newPostBlock.find('.post-description').text(description);
        newPostBlock.find('.post-location').attr('src', 'https://flagsapi.com/' + locationCode + '/flat/64.png');
        newPostBlock.find('.post-location').attr('title', location);
        let timestamp = new Date(dateOfCreation).getTime();
        let datetime = new Date(timestamp);
        newPostBlock.find('.post-create-time').text(datetime.toLocaleString());
        newPostBlock.find('.post-id').val(postId);
        let dropId = 'dropdownMenuButton' + postId;
        let dropdown = newPostBlock.find('.btn-dropdown-post');  
        dropdown.attr('id', dropId);
        let dropdownContent = newPostBlock.find('.dropdown-menu');
        dropdownContent.attr('aria-labelledby', dropId);
        let btnDelete = newPostBlock.find('.btn-post-delete');
        btnDelete.click(() => {
            deletePost(postId, newPostBlock);
        });
        let btnEdit = newPostBlock.find('.btn-post-edit');
        btnEdit.click(() => {
            editPost(postId, newPostBlock);
        });
        $('.posts').prepend(newPostBlock);
    }

    const deletePost = (id, post) => {
        hub.invoke('DeletePost', parseInt(id)).then((result) => {
                if (result) {
                    $(post).remove();
                }
            })
            .catch((resolve) => { console.log(resolve) });
    }

    const editPost = (id, post) => {
        let editTextarea = $('<textarea />', {
            'class': 'form-control ' + 'btn-post-description-' + id,
            rows: 3,
            'text': $(post).find('.post-description').text()
        });
        let description = post.find('.post-description');
        description.replaceWith(editTextarea);
        let editButton = post.append(`<div class="d-grid">
                        <button class="btn btn-primary btn-edit-save" type = "button">Сохранить изменения</button>
                    </div> `).children().last();

        post.find('.btn-edit-save').click((obj) => {
            let updateDescription = editTextarea.val();
            const updatePost = {
                PostId: parseInt(id),
                Description: updateDescription
            }
            hub.invoke('EditPost', { ...updatePost })
                .then((result) => {
                    if (result) {
                        let editDiv = $('<p />', {
                            'class': 'mb-3 tx-14 post-description',
                            'text': updateDescription
                        });
                        editTextarea.replaceWith(editDiv);
                        editButton.remove();
                    }
                });
        });
    }
});