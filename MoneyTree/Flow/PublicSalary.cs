namespace MoneyTree
{
    public class PublicSalary : MoneyFlowBase
    {
        public PublicSalary(decimal net, int months) : base(GetSalaryCost(net) * months)
        {
            this.Commissionaire = new SalaryTax(net,GetSalaryCost(net));
        }

        private static decimal GetSalaryCost(decimal net)
        {
            var gross = net / 0.78M;
            return new PublicSalaryTax().ApplyCommission(gross);
        }
        
        private class SalaryTax:TaxBase
        {
            public SalaryTax(decimal net,decimal gross):base()
            {
                this.Taux = 1-net/gross;
            }
        }
    }
}