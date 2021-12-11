namespace OptimisationTax
{
    public class PortageSalary : Salary
    {
        private readonly decimal percentCost;
        
        public PortageSalary(decimal net, int months, decimal percentCost) : base(net, months)
        {
            this.percentCost = percentCost;
        }

        public override decimal GetTotalAmount(int workingDays)
        {
            return base.GetTotalAmount(workingDays)-base.GetTotalAmount(workingDays)*(percentCost/100) ;
        }
    }
}