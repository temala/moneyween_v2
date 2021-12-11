using System.Collections.Generic;

namespace OptimisationTax
{
    public interface IEmployee : IHumanResource,IOutcomeSource
    {
        Salary Salary { get; }
        void AddInvoice(IInvoice source);

        decimal GetRecurrentIncomes(int workingDays);
    }
}