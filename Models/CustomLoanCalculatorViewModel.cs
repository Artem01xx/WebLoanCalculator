using System;
using System.ComponentModel.DataAnnotations;

public class CustomLoanCalculatorViewModel
    {
    public CustomLoanCalculatorViewModel()
    {
        LoanAmountCustom = 0;
        LoanTermDaysCustom = 0;
        InterestRatePerDayCustom = 0;
        PaymentStepDays = 0;
    }
    

    [Required(ErrorMessage = "Поле 'Сумма займа' обязательно для заполнения.")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле 'Сумма займа' должно быть положительным числом.")]
    public decimal LoanAmountCustom { get; set; }

    [Required(ErrorMessage = "Поле 'Срок займа (в днях)' обязательно для заполнения.")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле 'Срок займа' должно быть целым числом больше 0.")]
    public int LoanTermDaysCustom { get; set; }

    [Required(ErrorMessage = "Поле 'Ставка (в день)' обязательно для заполнения.")]
    [Range(0.01, 100, ErrorMessage = "Поле 'Ставака' должно быть в пределах от 1 до 100.")]
    public decimal InterestRatePerDayCustom { get; set; }

    [Required(ErrorMessage = "Поле 'Шаг платежа (в днях)' обязательно для заполнения.")]
    [Range(0.01, 100, ErrorMessage = "Поле 'Шаг платежа' должно быть в пределах от 1 до 100.")]
    public int PaymentStepDays { get; set; }
}

