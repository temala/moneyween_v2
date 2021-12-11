using System;

namespace OptimisationTax
{
    public interface IPeriodicIncomeSource:IIncomeSource
    {
        int Months { get; }
    }
}