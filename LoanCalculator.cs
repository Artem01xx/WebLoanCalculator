using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public  class LoanCalculatorServices : ILoanCalculator
{
    public  LoanCalculationResult Calculate(LoanCalculatorViewModel model)
    {
        var result = new LoanCalculationResult
        {
            Payments = new List<Payment>(),
            TotalInterest = CalculateTotalInterest(model.LoanAmount, model.LoanTermMonths * 30, model.InterestRate)
        };

        for (int i = 1; i <= model.LoanTermMonths; i++)
        {
            var payment = new Payment
            {
                PaymentNumber = i,
                PaymentDate = DateTime.Today.AddMonths(i).ToString("dd MMMM yyyy"),
                PrincipalPayment = CalculatePrincipalPayment(model.LoanAmount, i),
                InterestPayment = CalculateInterestPayment(model.LoanAmount, i, model.InterestRate),
                RemainingBalance = CalculateRemainingBalance(model.LoanAmount, i, model.LoanTermMonths)
            };
            result.Payments.Add(payment);
        }

        return result;
    }

    public LoanCalculationResult CalculateCustom(CustomLoanCalculatorViewModel model)
    {
        var custom_result = new LoanCalculationResult
        {
            CustomPayment = new List<CustomPayment>(),
            TotalInterest = CalculateTotalInterest(model.LoanAmountCustom, model.LoanTermDaysCustom, model.InterestRatePerDayCustom)
        };

        decimal remainingBalance = model.LoanAmountCustom;
        decimal monthlyInterestRate = model.InterestRatePerDayCustom / 100 / 30; // Преобразуем годовую ставку в месячную

        // Рассчитываем коэффициент аннуитета
        decimal annuityCoefficient = (monthlyInterestRate * (decimal)Math.Pow((double)(1 + monthlyInterestRate), model.LoanTermDaysCustom))
                                     / ((decimal)Math.Pow((double)(1 + monthlyInterestRate), model.LoanTermDaysCustom) - 1);

        for (int i = 1; i <= model.LoanTermDaysCustom; i += model.PaymentStepDays)
        {
            // Расчет процентной составляющей
            decimal interestPayment = remainingBalance * monthlyInterestRate;

            // Расчет ежемесячного платежа
            decimal monthlyPayment = annuityCoefficient * remainingBalance;

            // Расчет части, идущей на погашение основного долга
            decimal principalPayment = monthlyPayment - interestPayment;

            var custom_payments = new CustomPayment
            {
                CustomPaymentNumber = i,
                CustomPaymentDateString = DateTime.Today.AddDays(i).ToString("dd MMMM yyyy"),
                AmounOfPayment = Math.Round(monthlyPayment, 2),
                MainDebt = Math.Round(principalPayment, 2),
                Procents = Math.Round(interestPayment, 2),
                CustomRemainingBalance = Math.Round(remainingBalance, 2),
            };

            remainingBalance -= principalPayment;
            custom_result.CustomPayment.Add(custom_payments);
        }

        custom_result.TotalInterest = Math.Round(custom_result.TotalInterest, 2);

        return custom_result;
    }

    //_______________Расчёты калькулятора___________________//



    // Реальные расчеты общих процентов
    private static decimal CalculateTotalInterest(decimal loanAmount, int loanTermDays, decimal annualInterestRate)
    {
        decimal dailyInterestRate = annualInterestRate / 100 / 365;
        decimal totalInterest = dailyInterestRate * loanAmount * loanTermDays;
        totalInterest = Math.Round(totalInterest, 2);
        return totalInterest;
    }

    private static decimal CalculatePrincipalPayment(decimal loanAmount, int loanTermMonths)
    {
        // Реальные расчеты платежей по телу
        return Math.Round(loanAmount / loanTermMonths, 2);
    }

    private static decimal CalculateInterestPayment(decimal loanAmount, int currentPaymentNumber, decimal monthlyInterestRate)
    {
        // Реальные расчеты платежей по процентов
        return Math.Round(loanAmount * monthlyInterestRate * (1 - (decimal)Math.Pow((double)(1 + monthlyInterestRate), - currentPaymentNumber)), 2);
    }

    private static decimal CalculateRemainingBalance(decimal loanAmount, int currentPaymentNumber, int loanTermMonths)
    {
        // Реальные расчеты оставшегося баланса
        decimal principalPayment = CalculatePrincipalPayment(loanAmount, loanTermMonths);
        decimal remainingBalance = loanAmount - (principalPayment * currentPaymentNumber);
        if(remainingBalance < 0)
            remainingBalance = 0;
        return Math.Round(remainingBalance, 3);
    }

}


public class LoanCalculationResult
{
    public List<CustomPayment>? CustomPayment { get; set; }
    public List<Payment>? Payments { get; set; }
    public decimal TotalInterest { get; set; }
}

public class Payment
{
    public int PaymentNumber { get; set; }
    public string PaymentDate { get; set; } = string.Empty;
    public decimal PrincipalPayment { get; set; }
    public decimal InterestPayment { get; set; }
    public decimal RemainingBalance { get; set; }
}

public class CustomPayment
{
    public int CustomPaymentNumber { get; set; }
    public string CustomPaymentDateString { get; set; } = string.Empty;
    public decimal AmounOfPayment { get; set; }
    public decimal MainDebt { get; set; }
    public decimal Procents { get; set; }
    public decimal CustomRemainingBalance { get; set; }
}