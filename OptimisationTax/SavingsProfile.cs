using System;
using System.Collections.Generic;
using System.Linq;

namespace OptimisationTax
{
    public class RecurrentSpending : IOutcomeSource
    {
        private decimal cost;
        private int months;

        public RecurrentSpending(decimal cost, int months)
        {
            this.cost = cost;
            this.months = months;
        }

        public decimal GetTotalCost(int workingDays)
        {
            return this.cost * this.months;
        }
    }
    public class SavingsProfile
    {
        private readonly IEmployee person;

        public SavingsProfile(IEmployee person)
        {
            this.person = person;
         
            this.Spendings = new List<IOutcomeSource>();
        }   

        public ICollection<IOutcomeSource> Spendings { get; }
        public decimal GetSavings(int workingDays)
        {
            var fixedSpendings = Spendings.Sum(s=>s.GetTotalCost(workingDays));
            
            if (person.GetRecurrentIncomes(workingDays) < fixedSpendings)
            {
                throw new Exception("Impossible to save money");
            }
            
            return this.person.GetBankBalance(workingDays) - fixedSpendings;
        }
        
    }
}