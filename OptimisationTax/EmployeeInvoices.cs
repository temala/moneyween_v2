namespace OptimisationTax
{
    public class EmployeeInvoices : IInvoice
    {
        private readonly decimal amount;
        
        public EmployeeInvoices(decimal amount, int months)
        {
            this.amount = amount;
            this.Months = months;
        }
        public int Months { get; }

        public decimal GetTotalAmount(int workingDays)
        {
            return amount * Months;
        }

        public decimal GetTotalCost(int workingDays)
        {
            return GetTotalAmount(workingDays);
        }
    }
}