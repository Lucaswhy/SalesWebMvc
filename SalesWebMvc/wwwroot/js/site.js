var year = new Date();
document.getElementById("dtFooter").innerHTML = ('&copy' + year.getFullYear() + "- BTS - com Your best trade store!");

$('#recipeCarousel').carousel({
    interval: 3000
})

$('.carousel .carousel-item').each(function () {
    var next = $(this).next();
    if (!next.length) {
        next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));

    for (var i = 0; i < 2; i++) {
        next = next.next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }

        next.children(':first-child').clone().appendTo($(this));
    }
});


function sortList() {

    var sortValue = (document.getElementById("slSort").value).toString();
    var filterValue = (document.getElementById("iFilter").value).toString();

    switch (sortValue) {
        case 'A - Z':
            sortValue = "";
            break;
        case 'Z - A':
            sortValue = "name_desc";
            break;
        case 'Price - low to high':
            sortValue = "Price";
            break;
        case 'Price - high to low':
            sortValue = "price_desc";
            break;
    }

    var query = new URLSearchParams();

    query.append("sortOrder", sortValue);

    if (filterValue != "") {
        query.append("currentFilter", filterValue);
    }

    var url = "/" + "?" + query.toString() + "&page=%2FIndex";

    window.location.href = url;

}

function setList() {
    var sortValue = "";

    console.log('here');

    var url = window.location.href;

    if (url.includes("name_desc")) {
        sortValue = 'Z - A';
    } else if (url.includes("Price")) {
        sortValue = 'Price - low to high';
    } else if (url.includes("price_desc")) {
        sortValue = 'Price - high to low';
    } else {
        sortValue = 'A - Z';
    }

    document.getElementById("slSort").value = sortValue;
    console.log(document.getElementById("slSort").value);
}