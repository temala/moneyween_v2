using System.IO;

namespace OptimisationTax
{
    public class ShareTax : ITax
    {
        private readonly CompanyShares shares;

        public ShareTax(CompanyShares shares)
        {
            this.shares = shares;
        }

        public decimal GetTotalCost(int workingDays)
        {
            return shares.GetTotalAmountBeforeTax(workingDays) * 0.3M;
        }
    }
    public class CompanyShares : IIncomeSource
    {
        private readonly decimal percent;
        private readonly ICompany company;
        private readonly ShareTax shareTax;
        
        public CompanyShares(ICompany company,decimal percent)
        {
            if (percent > 100)
            {
                throw new InvalidDataException("percentage should be between 0 and 100");
            }
            
            this.company = company;
            this.percent = (percent/100);
            this.shareTax = new ShareTax(this);
        }
        public decimal GetTotalAmount(int workingDays)
        {
            return GetTotalAmountBeforeTax(workingDays) - this.shareTax.GetTotalCost(workingDays);
        }

        public decimal GetTotalAmountBeforeTax(int workingDays)
        {
            return this.company.GetBalanceAfterTax(workingDays) * percent;
        }
    }
}