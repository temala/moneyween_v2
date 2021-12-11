using System;

namespace OptimisationTax
{
    public interface IHumanResource
    {
        decimal GetRevenu(int workingDays);

        decimal GetBankBalance(int workingDays);
    }
}