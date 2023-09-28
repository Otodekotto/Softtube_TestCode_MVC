$(document).ready(function () {
    var currentPage = 0; // Track the current page
    var itemsPerPage = 10; // Number of items per page
    var totalPages = Math.ceil(totalCount / itemsPerPage); // Calculate the total number of pages

    function updateView() {
        var startIndex = currentPage * itemsPerPage;
        var endIndex = startIndex + itemsPerPage;
        console.log("Total Pages: ", totalPages);
        console.log("Start Index: ", startIndex);
        console.log("End Index: ", endIndex);

        // Hide all items
        $('.product-item').hide();

        // Show items for the current page
        $('.product-item').slice(startIndex, endIndex).show();
    }

    // Initialize the view
    updateView();

    // Handle next page button click
    $('#nextPage').click(function () {
        if (currentPage < totalPages - 1) { // Check if not on the last page
            currentPage++;
            updateView();
        }
    });

    // Handle previous page button click
    $('#prevPage').click(function () {
        if (currentPage > 0) {
            currentPage--;
            updateView();
        }
    });
});