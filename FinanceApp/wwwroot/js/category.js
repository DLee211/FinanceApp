//Create a new category
document.addEventListener("DOMContentLoaded", function() {
    document.getElementById("saveCategoryButton").addEventListener("click", function() {
        var categoryName = document.getElementById("categoryName").value;

        if (!categoryName) {
            alert("Name is required.");
            return;
        }
        if (categoryName.length > 20) {
            alert("Name cannot be longer than 20 characters.");
            return;
        }

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

//Edit a category
document.addEventListener("DOMContentLoaded", function() {
    var editLinks = document.querySelectorAll('.edit-link');

    editLinks.forEach(function(link) {
        link.addEventListener("click", function(event) {
            event.preventDefault();

            var categoryId = this.dataset.id;
            var categoryName = this.parentElement.previousElementSibling.textContent.trim();

            document.getElementById("categoryId").value = categoryId;
            document.getElementById("categoryName").value = categoryName;

            $('#editCategoryModal').modal('show');
        });
    });

    document.getElementById("saveEditsCategoryButton").addEventListener("click", function() {
        var categoryId = document.getElementById("categoryId").value;
        var categoryName = document.getElementById("editCategoryName").value;

        if (!categoryName) {
            alert("Name is required.");
            return;
        }
        if (categoryName.length > 20) {
            alert("Name cannot be longer than 20 characters.");
            return;
        }

        console.log("Button clicked. Category name: " + categoryName); // Debugging line

        fetch('/Category/Edit/' + categoryId, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({
                Id: categoryId,
                Name: categoryName,
                Transactions: []
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                var contentType = response.headers.get("content-type");
                if(contentType && contentType.includes("application/json")) {
                    return response.json();
                } else {
                    console.log("Oops, we haven't got JSON! Here's what we got instead: ");
                    return response.text();
                }
            })
            .then(data => {
                console.log("Fetch request successful. Hiding the modal."); // Debugging line
                $('#editCategoryModal').modal('hide');
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    });
    
    $('#editCategoryModal').on('hidden.bs.modal', function (e) {
    location.reload();
    });
});

//Delete a category
document.addEventListener("DOMContentLoaded", function() {
    var deleteLinks = document.querySelectorAll('.delete-link');

    deleteLinks.forEach(function(link) {
        link.addEventListener("click", function(event) {
            event.preventDefault();

            var deleteForm = document.getElementById("deleteCategoryForm");
            deleteForm.action = '/Category/Delete/' + this.dataset.id;

            $('#deleteCategoryModal').modal('show');
        });
    });

    document.getElementById("confirmDeleteButton").addEventListener("click", function() {
        document.getElementById("deleteCategoryForm").submit();
    });
});


document.addEventListener("DOMContentLoaded", function() {
    // Event listeners for the "x" button and the "Close" button in the Edit modal
    var editModalCloseButtons = document.querySelectorAll('#editCategoryModal .close, #editCategoryModal .btn-secondary');
    editModalCloseButtons.forEach(function(button) {
        button.addEventListener("click", function() {
            $('#editCategoryModal').modal('hide');
        });
    });

    // Event listeners for the "x" button and the "Close" button in the Delete modal
    var deleteModalCloseButtons = document.querySelectorAll('#deleteCategoryModal .close, #deleteCategoryModal .btn-secondary');
    deleteModalCloseButtons.forEach(function(button) {
        button.addEventListener("click", function() {
            $('#deleteCategoryModal').modal('hide');
        });
    });
});

//Details

document.addEventListener("DOMContentLoaded", function() {
    var detailsLinks = document.querySelectorAll('.details-link');

    detailsLinks.forEach(function(link) {
        link.addEventListener("click", function(event) {
            event.preventDefault();

            var categoryId = this.dataset.id;

            fetch('/Category/Details/' + categoryId)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    console.log("Data returned from server: ", data); // Debugging line
                    // Format the data
                    var formattedData = 'ID: ' + data.id + '<br><br>Name: ' + data.name;
                    // Populate the modal with the formatted data
                    document.getElementById("categoryDetails").innerHTML = formattedData;
                    // Show the modal
                    $('#detailsModal').modal('show');
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
        });
    });
});

document.addEventListener("DOMContentLoaded", function() {
    // Event listeners for the "x" button and the "Close" button in the Details modal
    var detailsModalCloseButtons = document.querySelectorAll('#detailsModal .close, #detailsModal .btn-secondary');
    detailsModalCloseButtons.forEach(function(button) {
        button.addEventListener("click", function() {
            $('#detailsModal').modal('hide');
        });
    });
});