using Microsoft.AspNetCore.Mvc;


    public class LoanCalculatorController : Controller
    {
    private readonly ILoanCalculator _loanCalculator;
    public LoanCalculatorController(ILoanCalculator loanCalculator)
    {
        _loanCalculator = loanCalculator;
    }
    // Передаём объект в представление Index
    public IActionResult Index()
    {
        var initialModel = new LoanCalculatorViewModel(); 
        return View(initialModel);
    }
    // Передаём объект в представление CustomCalculator
    public IActionResult CustomCalculator()
    {
        var model = new CustomLoanCalculatorViewModel();
        return View(model);
    }

    [HttpPost]
    public IActionResult Calculate(LoanCalculatorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var result = _loanCalculator.Calculate(model);
        ViewBag.MainLoanCalculator = result;
        return View("Results", result);
    }

    [HttpPost]
    public IActionResult CalculateCustom(CustomLoanCalculatorViewModel model) 
    { 
        if(!ModelState.IsValid) 
        {
            return View("CustomCalculator",  model);
        }
        var result = _loanCalculator.CalculateCustom(model);
        ViewBag.CustomLoanCalculator = result;
        return View("Results", result);
    }
}

