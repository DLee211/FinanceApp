﻿document.addEventListener("DOMContentLoaded", function() {
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