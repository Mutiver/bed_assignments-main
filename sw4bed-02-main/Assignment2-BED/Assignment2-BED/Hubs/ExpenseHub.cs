using Assignment2_BED.Controllers;
using Assignment2_BED.Models;
using Assignment2_BED.Hubs;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Assignment2_BED.Hubs
{
    [HubName("expenseHub")]
    public class ExpenseHub : Hub<IExpense>
    {
        public ExpenseHub() { }
        public async Task ExpenseUpdate(Expense expense)
        {
            await Clients.All.ExpenseUpdate(expense);
        }
    }
}

