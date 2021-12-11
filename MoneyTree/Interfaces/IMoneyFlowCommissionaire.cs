namespace MoneyTree
{
    public interface IMoneyFlowCommissionaire
    {
        IMoneyTreeNode Commissionaire { get; }
        decimal ApplyCommission(decimal gross);
    }
}