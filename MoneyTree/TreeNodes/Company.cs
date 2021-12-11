using System;
using System.Linq;

namespace MoneyTree
{
    public class Company : MoneyTreeNodeBase
    {
        public Company(string name) : base(name)
        {
        }

        public decimal GetMoneyBalanceBeforeShareDistribution()
        {
            var balance = Incomes.Sum(income => income.Net) -
                          Outcomes.Where(o => !(o is CompanyShares)).Sum(outcome => outcome.Gross);
            
            if (balance < 0)
                throw new Exception("BankRupt");

            return balance;
        }
    }
}