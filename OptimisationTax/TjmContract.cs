using System;

namespace OptimisationTax
{
    public class TjmContract : IContract
    {
        private readonly decimal amount;

        public TjmContract(decimal amount)
        {
            this.amount = amount;
        }
        
        public decimal GetTotalAmount(int workingDays)
        {
            return workingDays * amount;
        }
    }
}