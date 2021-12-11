namespace MoneyTree
{
    public class StateSalaryTax : TaxBase
    {
        public StateSalaryTax():base()
        {
            this.Taux = 0.15M;
        }
    }
}