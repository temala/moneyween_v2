namespace MoneyTree
{
    public class EmployeeInvoice : MoneyFlowBase
    {
        public EmployeeInvoice(decimal amount, int months =1)
            : base(amount * months)
        {
        }
    }
}