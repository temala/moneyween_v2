namespace MoneyTree
{
    public class CompanyConsultingContract : MoneyFlowBase
    {
        public CompanyConsultingContract(decimal tjm, decimal workingDays)
            : base(tjm * workingDays)
        {

        }
    }
}