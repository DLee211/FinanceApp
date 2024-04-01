//Create
document.addEventListener("DOMContentLoaded", function() {
    document.getElementById("saveTransactionButton").addEventListener("click", function() {
        var transactionName = document.getElementById("transactionName").value;
        var transactionValue = document.getElementById("transactionValue").value;
        var transactionDate = document.getElementById("transactionDate").value;
        var transactionCategory = document.getElementById("transactionCategory").value;

        // Add your validation logic here

        fetch('/Transaction/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({
                Name: transactionName,
                Value: transactionValue,
                Date: transactionDate,
                CategoryId: transactionCategory
            })
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
    });
});

document.addEventListener("DOMContentLoaded", function() {
    // Event listeners for the "x" button and the "Close" button in the Add Transaction modal
    var addModalCloseButtons = document.querySelectorAll('#addTransactionModal .close, #addTransactionModal .btn-secondary');
    addModalCloseButtons.forEach(function(button) {
        button.addEventListener("click", function() {
            $('#addTransactionModal').modal('hide');
        });
    });
});