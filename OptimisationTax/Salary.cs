using System;

namespace OptimisationTax
{
    public class Salary:IOutcomeSource, IPeriodicIncomeSource
    {
        public Salary(decimal net, int months)
        {
            Net = net;
            
            Months = months;
        }
        public int Months { get; }
        public decimal Cost
        {
            get => Net + Tax(Net);
        }
        public decimal Net { get; private set; }
        private decimal Tax(decimal netCost)
        {
            return (netCost / 0.556M)-netCost;
        }
        
        public decimal GetTotalCost(int workingDays)
        {
            return this.Months * this.Cost;
        }

        public virtual decimal GetTotalAmount(int workingDays)
        {
            return this.Months * this.Net;
        }

        public void SetSalaryIncrease(decimal amount)
        {
            this.Net += amount;
        }
    }
}