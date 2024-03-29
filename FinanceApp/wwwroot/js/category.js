document.addEventListener("DOMContentLoaded", function() {
    document.getElementById("saveCategoryButton").addEventListener("click", function() {
        var categoryName = document.getElementById("categoryName").value;

        console.log("Button clicked. Category name: " + categoryName); // Debugging line

        fetch('/Category/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({
                Name: categoryName,
                Transactions: []
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                console.log("Fetch request successful. Response status: " + response.status); // Debugging line
                return response.json();
            })
            .then(data => {
                // Refresh the page or update the category list
                console.log("Response data: " + JSON.stringify(data)); // Debugging line
                location.reload();
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    });
});