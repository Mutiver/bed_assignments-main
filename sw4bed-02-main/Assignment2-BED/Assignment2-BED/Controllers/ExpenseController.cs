using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2_BED.Data;
using Assignment2_BED.Hubs;
using Assignment2_BED.Models;
using Mapster;
using Assignment2_BED.Properties.Models;
using Microsoft.AspNetCore.SignalR;

namespace Assignment2_BED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ModelManageDB _context;
        private readonly IHubContext<ExpenseHub> _hubContext;

        public ExpenseController(ModelManageDB context, IHubContext<ExpenseHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseCreated>> PostExpense(ExpenseCreate expenseCreate)
        {
            var dbModel = _context.model.Find(expenseCreate.ModelId);
            var dbJob = _context.job.Find(expenseCreate.JobId);
            if (dbModel == null || dbJob == null)
            {
                string error = "";
                if (dbModel == null) error += "Model not found\n";
                if (dbJob == null) error += "Job not found\n";
                return NotFound(error);
            }

            _context.Entry(dbModel)
            .Collection(m => m.Expenses)
            .Load();

            _context.expense.Add(expenseCreate.Adapt<Expense>());
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("expenseAdded", expenseCreate);
            
            var dbExpenses = await _context.expense.ToListAsync();

            return Accepted(dbExpenses.Adapt<List<ExpenseCreated>>());
        }
    }
}
