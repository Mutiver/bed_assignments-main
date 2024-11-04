using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2_BED.Pages;

public class NewExpensesModel : PageModel
{
    private readonly ILogger<NewExpensesModel> _logger;
    public NewExpensesModel(ILogger<NewExpensesModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
}