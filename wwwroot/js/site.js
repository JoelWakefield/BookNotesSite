(async () => {
    const body = document.querySelector('body');

    body.addEventListener("click", (e) => {
        if ($(e.target).hasClass('get-book'))
            getItem(e.target);
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
            url: "/Home/Get/" + buttonId,
            success: function (partialView) {
                $('#details-box').html(partialView);
            }
        });
    }

    function removeAllChildNodes(parent) {
        while (parent.firstChild) {
            parent.removeChild(parent.firstChild);
        }
    }
})();