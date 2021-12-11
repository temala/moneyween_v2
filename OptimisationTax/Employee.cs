using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OptimisationTax
{
    public class Employee : IEmployee
    {
        private readonly decimal familyCoefficient;
        private readonly ICollection<IOutcomeSource> costs;
        private readonly ICollection<IIncomeSource> incomes;

        public Employee(Salary salary, decimal familyCoefficient)
        {
            this.Salary = salary;
            this.familyCoefficient = familyCoefficient;
            this.costs = new Collection<IOutcomeSource>();
            this.incomes = new Collection<IIncomeSource>();

            this.costs.Add(salary);
            this.incomes.Add(salary);
        }

        public Salary Salary { get; }

        public decimal GetRevenu(int workingDays)
        {
            return this.incomes.Sum(o => o.GetTotalAmount(workingDays));
        }
        
        public decimal GetTotalCost(int workingDays)
        {
            return this.costs.Sum(o => o.GetTotalCost(workingDays));
        }
        
        public decimal GetBankBalance(int workingDays)
        {
            var taxOnProfit = new TaxOnProfit(this,this.familyCoefficient);
            
            return GetRevenu(workingDays) - taxOnProfit.GetTotalCost(workingDays);
        }

        public void AddInvoice(IInvoice source)
        {
            this.incomes.Add(source);
            this.costs.Add(source);
        }

        public void AddShares(CompanyShares source)
        {
            this.incomes.Add(source);
        }

        public decimal GetRecurrentIncomes(int workingDays)
        {
            return this.incomes.OfType<IPeriodicIncomeSource>().Sum(income => income.GetTotalAmount(workingDays));
        }
        
    }
}