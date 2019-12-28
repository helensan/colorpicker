
// update the active page when user clicks on page indexer
$('.pageIdx').on('click', function (e) {
    e.preventDefault();
    if ($(this).hasClass('active')) {
        return;
    }
    var pageIdx = $(this).data("page");
    if ($(".page[data-page='" + pageIdx + "']").length > 0) {
        $('.page').removeClass('active');
        $(".page[data-page='" + pageIdx + "']").addClass('active');
        $('.pageIdx').removeClass('active');
        $(this).addClass('active');
    }
});