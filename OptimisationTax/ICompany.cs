using System;
using System.Collections;
using System.Collections.Generic;

namespace OptimisationTax
{
     public interface ICompany
     {
          ICollection<IIncomeSource> IncomeSources { get;}
     
          ICollection<IOutcomeSource> OutcomeSources { get; }
          
          ICollection<ITax> Taxes { get; }
          
          ICollection<IHumanResource> Employees { get; }

          decimal GetBalanceBeforeTax(int workingDays);
          
          decimal GetBalanceAfterTax(int workingDays);
     }
}