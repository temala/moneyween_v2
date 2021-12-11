using System;
using System.Collections.Generic;

namespace MoneyTree
{
    public interface IMoneyTreeNode
    {
        string Name { get; }
        ICollection<IMoneyFlow> Incomes { get; }
        ICollection<IMoneyFlow> Outcomes { get; }

        decimal GetMoneyBalance();
    }
}