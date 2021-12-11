using System;

namespace OptimisationTax
{
    public interface IOutcomeSource
    {
        decimal GetTotalCost(int workingDays);
    }
}