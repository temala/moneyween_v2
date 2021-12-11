using System;

namespace OptimisationTax
{
    public interface IPeriodicOutcomeSource:IOutcomeSource
    {
        TimeSpan Periodicity { get; }
    }
}