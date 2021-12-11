using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualBasic;

namespace OptimisationTax
{
    public class ContractBasedEmployee : Employee,IIncomeSource
    {
        public ICollection<IContract> Contracts { get; }
        
        public ContractBasedEmployee(Salary salary, decimal familyCoefficient) : base(salary, familyCoefficient)
        {
            Contracts = new Collection<IContract>();
        }

        public decimal GetTotalAmount(int workingDays)
        {
            return this.Contracts.Sum(c => c.GetTotalAmount(workingDays));
        }
    }
}