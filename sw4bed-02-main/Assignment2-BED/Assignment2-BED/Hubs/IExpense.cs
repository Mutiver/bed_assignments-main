using Assignment2_BED.Models;

namespace Assignment2_BED.Hubs
{
    public interface IExpense
    {
        public Task ExpenseUpdate(Expense expense);
    }
}