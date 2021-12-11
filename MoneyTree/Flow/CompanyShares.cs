namespace MoneyTree
{
    public class CompanyShares : MoneyFlowBase
    {
        private readonly Company company;

        public CompanyShares(Company company, decimal percentShares)
            : base(company.GetMoneyBalanceBeforeShareDistribution() * (percentShares/100))
        {
            this.company = company;
            this.Commissionaire = new PublicSharesTax();
        }
    }
}