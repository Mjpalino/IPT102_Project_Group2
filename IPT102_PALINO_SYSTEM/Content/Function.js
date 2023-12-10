document.addEventListener("DOMContentLoaded", function () {
    var dropdowns = document.getElementsByClassName("dropdown-btn");

    for (var i = 0; i < dropdowns.length; i++) {
        dropdowns[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var dropdownContent = this.nextElementSibling;
            if (dropdownContent.style.display === "block") {
                dropdownContent.style.display = "none";
            } else {
                dropdownContent.style.display = "block";
            }
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll('.sidenav a');

    links.forEach(function (link) {
        link.addEventListener('click', function () {
            // Remove active class from all links
            links.forEach(function (otherLink) {
                otherLink.classList.remove('active');
            });

            // Add active class to the clicked link
            this.classList.add('active');
        });
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll('.dropdown-container a');

    links.forEach(function (link) {
        link.addEventListener('click', function () {
            // Remove active class from all links
            links.forEach(function (otherLink) {
                otherLink.classList.remove('active');
            });

            // Add active class to the clicked link
            this.classList.add('active');
        });
    });
});

