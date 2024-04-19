$(document).ready(function () {
    $('.page').click(function onPageClick() {
        const page = $(this).data('page');
        navigateToPage(page);
    });

    function navigateToPage(page) {
        const baseUrl = document.URL;
        window.location.href = baseUrl.replace(/page=[0-9]+/, "page=" + page);
    }
});