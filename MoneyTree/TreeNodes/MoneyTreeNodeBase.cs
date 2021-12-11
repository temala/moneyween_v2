using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MoneyTree
{
    public abstract class MoneyTreeNodeBase : IMoneyTreeNode
    {
        public MoneyTreeNodeBase(string name)
        {
            this.Name = name;
            Incomes = new List<IMoneyFlow>();
            Outcomes = new List<IMoneyFlow>();
        }
        
        public string Name { get; }

        public ICollection<IMoneyFlow> Incomes { get; }
        public ICollection<IMoneyFlow> Outcomes { get; }

        public decimal GetMoneyBalance()
        {
            var balance =  Incomes.Sum(income => income.Net)-Outcomes.Sum(outcome=>outcome.Gross);
            if (balance < 0)
                throw new Exception("BankRupt");
            
            Console.WriteLine($"{Name}: {balance.ToString("c3",CultureInfo.CreateSpecificCulture("fr-FR"))}");
            return balance;
        }
    }
}