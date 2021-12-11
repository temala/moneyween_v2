using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OptimisationTax
{
    public abstract class Company:ICompany
    {
        protected Company()
        {
            IncomeSources = new Collection<IIncomeSource>();
            OutcomeSources = new Collection<IOutcomeSource>();
            Employees = new Collection<IHumanResource>();
            Taxes = new Collection<ITax>();
            Taxes.Add(new CompanyTax(this));
        }

        public ICollection<IIncomeSource> IncomeSources { get; }
        public ICollection<IOutcomeSource> OutcomeSources { get; }
        public ICollection<ITax> Taxes { get; }
        public ICollection<IHumanResource> Employees { get; }

        private decimal GetEmployeeCosts(int workingDays)
        {
            return (this.Employees.OfType<IOutcomeSource>()).Sum(employee =>
                employee.GetTotalCost(workingDays));
        }
        
        private decimal GetIncomeFromEmployee(int workingDays)
        {
            return (this.Employees.OfType<IIncomeSource>()).Sum(employee =>
                employee.GetTotalAmount(workingDays));
        }

        public decimal GetBalanceBeforeTax(int workingDays)
        {
            
            var costs =  OutcomeSources.Sum(o=>o.GetTotalCost(workingDays)) + GetEmployeeCosts(workingDays);
            var incomes = IncomeSources.Sum(o => o.GetTotalAmount(workingDays)) + GetIncomeFromEmployee(workingDays);
            var balance = incomes-costs;
            
            if (balance < 0)
            {
                throw new Exception("Bankrupt");
            }
            
            return balance;
        }
        
        public decimal GetBalanceAfterTax(int workingDays)
        {
            return GetBalanceBeforeTax(workingDays) - this.Taxes.Sum(tax => tax.GetTotalCost(workingDays));
        }
    }

    public class SasuCompany : Company
    {
    }
}