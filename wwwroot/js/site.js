(async () => {
    const body = document.querySelector('body');

    body.addEventListener("click", (e) => {
        console.log(e);
        if ($(e.target).hasClass('get-book'))
            getItem(e.target);
        else if ($(e.target).hasClass('add-book'))
            addBook();
        else if ($(e.target).hasClass('remove-book'))
            removeBook();
        else if ($(e.target).hasClass('add-note'))
            addNote(e.target);
        else if ($(e.target).hasClass('remove-note'))
            removeNote(e.target);

    });


    function getItem(element) {
        //  clear container
        removeAllChildNodes(document.getElementById('details-box'));

        //  pull the book data via id
        var buttonId = $(element).attr("id");
        console.log(buttonId);

        $.ajax({
            async: true,
            type: "GET",
            url: "/Book/Get/" + buttonId,
            success: function (partialView) {
                $('#details-box').html(partialView);
            }
        });
    }

    function addBook() {
        //  clear container
        removeAllChildNodes(document.getElementById('details-box'));

        //  add the book editor partial view to the new book element
        $.ajax({
            async: true,
            type: "GET",
            url: "/Book/Create/",
            success: function (partialView) {
                $('#details-box').html(partialView);
            }
        });
    }

    function removeBook() {
        const container = document.getElementById('details-box');
        removeAllChildNodes(container);
    }

    function addNote(element) {
        //  add a note editor partial view to the calling note's subnotes section
    }

    function removeNote(element) {
        //  remove note from parent element
        removeAllChildNodes(element);
    }

    function removeAllChildNodes(parent) {
        while (parent.firstChild) {
            parent.removeChild(parent.firstChild);
        }
    }
})();