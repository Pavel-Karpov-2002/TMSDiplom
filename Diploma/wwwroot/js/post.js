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
            addPostOnPage(post.id, post.creatorUserName, post.creatorAvatarUrl, post.description, post.dateOfCreation);
        });
    });

    hub.on('UserGotNewPost', (post) => {
        addPostOnPage(post.id, post.creatorUserName, post.creatorAvatarUrl, post.description, post.dateOfCreation);
    });

    hub.start().then(() => {
        let userId = window.location.pathname.split('/').pop();
        hub.invoke('GetLastUserPosts', parseInt(userId));
    });
   
    const addPostOnServer = (post) => {
        hub.invoke('AddNewPost', {...post});
    };

    const addPostOnPage = (postId, userName, userAvatarUrl, description, dateOfCreation) => {
        const newPostBlock = $('.post.post-template').clone();
        newPostBlock.removeClass('post-template');
        newPostBlock.find('.post-creator').text(userName);
        newPostBlock.find('.user-src-avatarUrl').attr('src', userAvatarUrl);
        newPostBlock.find('.post-description').text(description);
        var timestamp = new Date(dateOfCreation).getTime();
        var datetime = new Date(timestamp);
        newPostBlock.find('.post-create-time').text(datetime.toLocaleString());
        newPostBlock.find('.post-id').val(postId);
        var dropId = 'dropdownMenuButton' + postId;
        var dropdown = newPostBlock.find('.btn-dropdown-post');  
        dropdown.attr('id', dropId);
        var dropdownContent = newPostBlock.find('.dropdown-menu');
        dropdownContent.attr('aria-labelledby', dropId);
        var btnDelete = newPostBlock.find('.btn-post-delete');
        btnDelete.click(() => {
            deletePost(postId, newPostBlock);
        });
        var btnEdit = newPostBlock.find('.btn-post-edit');
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
        var editTextarea = $('<textarea />', {
            'class': 'form-control ' + 'btn-post-description-' + id,
            rows: 3,
            'text': $(post).find('.post-description').text()
        });
        var description = post.find('.post-description');
        description.replaceWith(editTextarea);
        var editButton = post.append(`<div class="d-grid">
                        <button class="btn btn-primary btn-edit-save" type = "button">Сохранить изменения</button>
                    </div> `).children().last();

        post.find('.btn-edit-save').click((obj) => {
            var updateDescription = editTextarea.val();
            const updatePost = {
                PostId: parseInt(id),
                Description: updateDescription
            }
            hub
                .invoke('EditPost', { ...updatePost })
                .then((result) => {
                    if (result) {
                        var editDiv = $('<p />', {
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