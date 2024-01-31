// Models/LoanCalculatorViewModel.cs
using System;
using System.ComponentModel.DataAnnotations;

public class LoanCalculatorViewModel
{
    public LoanCalculatorViewModel()
    {
        LoanAmount = 0;
        LoanTermMonths = 0;
        InterestRate = 0;
    }
    [Required(ErrorMessage = "Введите сумму займа")]
    [Range(1, double.MaxValue, ErrorMessage = "Поле 'Сумма займа' должно быть положительным числом.")]
    public decimal LoanAmount { get; set; }

    [Required(ErrorMessage = "Введите срок займа")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле 'Срок займа' должно быть целым числом больше 0.")]
    public int LoanTermMonths { get; set; }

    [Required(ErrorMessage = "Введите ставку")]
    [Range(0.01, 100, ErrorMessage = "Поле 'Ставка' должно быть в пределах от 0.01 до 100.")]
    public decimal InterestRate { get; set; }


}


