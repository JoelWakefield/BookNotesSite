(async () => {
    var pageIsOpen = false;

    const body = document.querySelector('body');
    const headerBar = document.getElementById('header-bar');
    const bookshelf = document.getElementById('bookshelf');
    const detailsBox = document.getElementById('details-box');

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
            $('#bookshelf').css('display', 'none');

            headerBar.classList.remove('header-slide-in');
            //bookshelf.classList.remove('bookshelf-fade-in');
            detailsBox.classList.remove('detail-slide-out');

            void headerBar.offsetWidth;
            //void bookshelf.offsetWidth;
            void detailsBox.offsetWidth;

            headerBar.classList.add('header-slide-out');
            //bookshelf.classList.add('bookshelf-fade-out');
            detailsBox.classList.add('detail-slide-in');

            $(headerBar).css('backgroundColor', '#f8feff');
        } else {
            headerBar.classList.remove('header-slide-out');
            //bookshelf.classList.remove('bookshelf-fade-out');
            detailsBox.classList.remove('detail-slide-in');

            void headerBar.offsetWidth;
            //void bookshelf.offsetWidth;
            void detailsBox.offsetWidth;

            headerBar.classList.add('header-slide-in');
            //bookshelf.classList.add('bookshelf-fade-in');
            detailsBox.classList.add('detail-slide-out');

            $(headerBar).css('backgroundColor', '#a9d7e0');
            $('#bookshelf').css('display', 'flex');
        }

        pageIsOpen = !pageIsOpen;
    }


    function removeAllChildNodes(parent) {
        while (parent.firstChild) {
            parent.removeChild(parent.firstChild);
        }
    }
})();