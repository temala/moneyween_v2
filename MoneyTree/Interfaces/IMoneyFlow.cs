using System.Collections.Generic;

namespace MoneyTree
{
    public interface IMoneyFlow
    {
        IMoneyFlowCommissionaire Commissionaire { get; set;}
        decimal Gross { get; }   
        decimal Net { get; }   
    }
}