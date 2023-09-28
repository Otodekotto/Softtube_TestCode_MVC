$(document).ready(function () {

    var currentPage = 0; // Track the current page
    var itemsPerPage = 10; // Number of items per page
    var totalPages = Math.ceil(totalCount / itemsPerPage); // Calculate the total number of pages


    function handleSearch() {
        var searchQuery = $('#searchInput').val().toLowerCase();

        // Hide all items
        $('.product-item').hide();

        if (searchQuery !== '') {
            // Filter items that match the search query
            var filteredItems = $('.product-item').filter(function () {
                var productName = $(this).find('h3').text().toLowerCase();
                return productName.includes(searchQuery);
            });

            // Display only 10 items per page for search results
            var startIndex = currentPage * itemsPerPage;
            var endIndex = startIndex + itemsPerPage;
            var visibleItems = filteredItems.slice(startIndex, endIndex);
            visibleItems.show();

            // Enable/disable next and previous buttons based on the number of search results
            $('#nextPage').prop('disabled', endIndex >= filteredItems.length);
            $('#prevPage').prop('disabled', currentPage === 0);
        } else {
            // If the search input is empty, show only items for the current page
            var startIndex = currentPage * itemsPerPage;
            var endIndex = Math.min(startIndex + itemsPerPage, totalCount);
            $('.product-item').slice(startIndex, endIndex).show();

            // Enable/disable next and previous buttons based on the number of items
            $('#nextPage').prop('disabled', endIndex >= totalCount);
            $('#prevPage').prop('disabled', currentPage === 0);
        }
    }

    // Handle search button click
    $('#searchButton').click(function () {
        currentPage = 0; // Reset the current page when a new search is initiated
        handleSearch();
    });

    function updateView() {
        handleSearch(); // Apply the search filter when updating the view
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