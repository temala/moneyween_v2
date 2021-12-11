using System;

namespace OptimisationTax
{
    public interface IIncomeSource
    {
        decimal GetTotalAmount(int workingDays);
    }
}