(async () => {
    var pageIsOpen = false;

    const body = document.querySelector('body');
    const detailsBox = document.getElementById('details-box');
    const headerBar = document.getElementById('header-bar');

    body.addEventListener("click", (e) => {
        if ($(e.target).hasClass('get-book'))
            getItem(e.target);
        else if ($(e.target).hasClass('open-menu'))
            returnToMenu();
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
                $(detailsBox).html(partialView);
                if (!pageIsOpen)
                    animatePage(true);
            }
        });
    }

    function returnToMenu() {
        if (pageIsOpen)
            animatePage(false);
    }

    function animatePage(openPage) {
        //  if direction == false, then reverse the animations
        if (openPage) {
            detailsBox.classList.remove('detail-slide-out');
            headerBar.classList.remove('header-slide-in');

            void detailsBox.offsetWidth;
            void headerBar.offsetWidth;

            detailsBox.classList.add('detail-slide-in');
            headerBar.classList.add('header-slide-out');

            $(headerBar).css('backgroundColor', 'aqua');
        } else {
            detailsBox.classList.remove('detail-slide-in');
            headerBar.classList.remove('header-slide-out');

            void detailsBox.offsetWidth;
            void headerBar.offsetWidth;

            detailsBox.classList.add('detail-slide-out');
            headerBar.classList.add('header-slide-in');

            $(headerBar).css('backgroundColor', 'deepskyblue');
        }

        pageIsOpen = !pageIsOpen;
    }


    function removeAllChildNodes(parent) {
        while (parent.firstChild) {
            parent.removeChild(parent.firstChild);
        }
    }
})();