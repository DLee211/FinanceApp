﻿document.addEventListener("DOMContentLoaded", function() { 
    document.getElementById("saveTransactionButton").addEventListener("click", function() { event.preventDefault();
    event.preventDefault();

        var transactionName = document.getElementById("transactionName").value;
        var transactionValue = document.getElementById("transactionValue").value;
        var transactionDate = document.getElementById("transactionDate").value;
        var transactionCategory = document.getElementById("transactionCategory");
        var transactionCategoryValue = transactionCategory.value;

        fetch('/Category/Details/' + transactionCategoryValue)
            .then(response => response.json())
            .then(data => {
                var transactionCategoryName = data.name;

                var transaction = {
                    Name: transactionName,
                    Value: transactionValue,
                    Date: transactionDate,
                    CategoryId: transactionCategoryValue,
                    Category: {
                        Name: transactionCategoryName
                    }
                };

                console.log("Transaction Name: ", transactionName);
                console.log("Transaction Value: ", transactionValue);
                console.log("Transaction Date: ", transactionDate);
                console.log("Transaction Category: ", transactionCategory);

                if (!transactionName) {
                    alert("Name is required.");
                    return;
                }
                if (transactionName.length > 20) {
                    alert("Name cannot be longer than 20 characters.");
                    return;
                }

                if (transactionValue.value <= 0 || !transactionValue)
                {
                    alert("Value cannot be null and must be greater than 0");
                    return;
                }
                if (!transactionDate)
                {
                    alert("Date is required");
                    return;
                }

                console.log("Button clicked. Transaction name: " + transactionName); // Debugging line

                fetch('/Transaction/Create', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                    },
                    body: JSON.stringify(transaction)
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error(`HTTP error! status: ${response.status}`);
                        }
                        console.log("Fetch request successful. Response status: " + response.status); // Debugging line
                        return response.json();
                    })
                    .then(data => {
                        console.log("Response data: " + JSON.stringify(data)); // Debugging line
                        location.reload();
                    })
                    .catch((error) => {
                        console.error('Error:', error);
                    });
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    });

    // Event listeners for the "x" button and the "Close" button in the Add Transaction modal
    var addModalCloseButtons = document.querySelectorAll('#addTransactionModal .close, #addTransactionModal .btn-secondary');
    addModalCloseButtons.forEach(function(button) {
        button.addEventListener("click", function() {
            $('#addTransactionModal').modal('hide');
        });
    });
});

//Delete a Transaction
document.addEventListener("DOMContentLoaded", function() {
    var deleteLinks = document.querySelectorAll('.delete-link');

    deleteLinks.forEach(function(link) {
        link.addEventListener("click", function(event) {
            event.preventDefault();

            var deleteForm = document.getElementById("deleteTransactionForm");
            deleteForm.action = '/Transaction/Delete/' + this.dataset.id;

            $('#deleteTransactionModal').modal('show');
        });
    });

    document.getElementById("confirmDeleteButton").addEventListener("click", function() {
        document.getElementById("deleteTransactionForm").submit();
    });
});

// Event listeners for the "x" button and the "Close" button in the Delete modal
var deleteModalCloseButtons = document.querySelectorAll('#deleteTransactionModal .close, #deleteTransactionModal .btn-secondary');
deleteModalCloseButtons.forEach(function(button) {
    button.addEventListener("click", function() {
        $('#deleteTransactionModal').modal('hide');
    });
});

//Details

document.addEventListener("DOMContentLoaded", function() {
    var detailsLinks = document.querySelectorAll('.details-link');

    detailsLinks.forEach(function(link) {
        link.addEventListener("click", function(event) {
            event.preventDefault();

            var transactionId = this.dataset.id;

            fetch('/Transaction/Details/' + transactionId)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    console.log("Data returned from server: ", data); // Debugging line
                    // Format the data
                    var formattedData = 'ID: ' + data.id + '<br><br>Name: ' + data.name + '<br><br>Value: ' + data.value + '<br><br>Date: ' + new Date(data.date).toLocaleString() + '<br><br>Category: ' + data.category.name;
                    document.getElementById("transactionDetails").innerHTML = formattedData;
                    // Show the modal
                    $('#detailsModal').modal('show');
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
        });
    });
});


//Edit a transaction

document.addEventListener("DOMContentLoaded", function() {
    var editLinks = document.querySelectorAll('.edit-link');
    

    editLinks.forEach(function(link) {
        link.addEventListener("click", function(event) {
            event.preventDefault();
            // Get the transactionId from the data-id attribute of the clicked link
            var transactionId = this.dataset.id;

            // Set the transactionId in the hidden input field in the modal
            document.getElementById('transactionId').value = transactionId;
            $('#editTransactionModal').modal('show');
        });
    });

    document.getElementById("saveEditsTransactionButton").addEventListener("click", function() {
        var transactionId = document.getElementById("transactionId").value;
        var transactionName = document.getElementById("editTransactionName").value;
        var transactionValue = document.getElementById("editTransactionValue").value;
        var transactionDate = document.getElementById("editTransactionDate").value;
        var transactionCategory = document.getElementById("editTransactionCategory");
        var transactionCategoryValue = transactionCategory.value;

        fetch('/Category/Details/' + transactionCategoryValue)
            .then(response => response.json())
            .then(data => {
                var transactionCategoryName = data.name;

                var transaction = {
                    Id: transactionId,
                    Name: transactionName,
                    Value: transactionValue,
                    Date: transactionDate,
                    CategoryId: transactionCategoryValue,
                    Category: {
                        Name: transactionCategoryName
                    }
                };


                console.log("Transaction Id: ", transactionId);
                console.log("Transaction Name: ", transactionName);
                console.log("Transaction Value: ", transactionValue);
                console.log("Transaction Date: ", transactionDate);
                console.log("Transaction Category: ", transactionCategory);

                if (!transactionName) {
                    alert("Name is required.");
                    return;
                }
                if (transactionName.length > 20) {
                    alert("Name cannot be longer than 20 characters.");
                    return;
                }

                if (transactionValue.value <= 0 || !transactionValue) {
                    alert("Value cannot be null and must be greater than 0");
                    return;
                }
                if (!transactionDate) {
                    alert("Date is required");
                    return;
                }

                fetch('/Transaction/Edit/' + transactionId, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                    },
                    body: JSON.stringify(transaction)
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error(`HTTP error! status: ${response.status}`);
                        }
                        return response.json();
                    })
                    .then(data => {
                        location.reload();
                    })
                    .catch((error) => {
                        console.error('Error:', error);
                    });
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    });

    $('#editCategoryModal').on('hidden.bs.modal', function (e) {
        location.reload();
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

document.addEventListener("DOMContentLoaded", function() {
    // Event listeners for the "x" button and the "Close" button in the Edit modal
    var editModalCloseButtons = document.querySelectorAll('#editTransactionModal .close, #editTransactionModal .btn-secondary');
    editModalCloseButtons.forEach(function(button) {
        button.addEventListener("click", function() {
            $('#editTransactionModal').modal('hide');
        });
    });
});