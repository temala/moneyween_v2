using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OptimisationTax
{
    public interface ITax
    {
        decimal GetTotalCost(int workingDays);
    }
    
    public class CompanyTax:ITax
    {
        private ICompany company;
        private const decimal TrancheImpotReduit = 38.120M;
        private const decimal CoefficientImpositionReduit = 0.15M;
        private const decimal CoefficentImposition = 0.265M;

        public CompanyTax(ICompany company)
        {
            this.company = company;
        }

        public decimal GetTotalCost(int workingDays)
        {
            var companyBalance = this.company.GetBalanceBeforeTax(workingDays);
            
            var tax = Math.Min(TrancheImpotReduit,companyBalance)  * CoefficientImpositionReduit;
                
            if (companyBalance > TrancheImpotReduit)
            {
                tax+=(companyBalance-TrancheImpotReduit)*CoefficentImposition;
            }
                
            return tax;
        }
    }
}