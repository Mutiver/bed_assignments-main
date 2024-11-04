//inspiration taken from martin stokholm
var connection = new signalR.HubConnectionBuilder().withUrl("/expenseHub").build();
connection.on("expenseAdded", function (expense)
{
    console.log("New Expense");
    var expenseObject = JSON.stringify(expense);
    var expenseItem = document.createElement("li");
    
    expenseItem.textContent = expenseObject;
    document.getElementById("expensesList").appendChild(expenseItem);
});

connection.start().then(function (){})
    .catch(function (err) {
});