$("#searchBar").autocomplete({ 
    source:
        function (request, response) {
            $.ajax({
                url: "/Home/Search",
                dataType: "json",
                data: {
                    term: request.term
                },
                success: function (data) {
                    var d = []; var label;
                    $.each(data, function (idx, elem) {
                        label = elem.name;
                        if (elem.name.indexOf(request.term) === -1) {
                            label = elem.hexCode.toLowerCase();
                        }
                        d.push({ id: elem.hexCode, label: label });
                    });
                    if (d.length <= 0) {
                        d = [{ id: null, label: 'No Results Found' }]; // Add default message if no matching results
                    }
                    response(d);
                }
            });
        },
    position: {
        of: '#searchBarContainer',
        my: 'left top',
        at: 'left bottom'
    },
    minLength: 3, // set minimum to 3 chars
    select: function (event, ui) {
        if (ui.item.id !== null) {
            window.location = '/Home/Detail?hexCode=' + ui.item.id.replace('#', '%23');
        }
    }
});

$.ui.autocomplete.prototype._resizeMenu = function () { // this chunck will resize the drop down when the window resizes
    var ul = this.menu.element;
    ul.outerWidth(this.element.outerWidth());
};
