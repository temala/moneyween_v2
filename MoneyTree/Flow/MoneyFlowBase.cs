using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MoneyTree
{
    public abstract class MoneyFlowBase : IMoneyFlow
    {
        protected MoneyFlowBase(decimal gross)
        {
            Gross = gross;
            //Console.WriteLine($"{this.GetType().Name}: {gross.ToString("c3",CultureInfo.CreateSpecificCulture("fr-FR"))}");
        }

        public IMoneyFlowCommissionaire Commissionaire { get; set; }
        public decimal Gross { get; }
        public decimal Net => Commissionaire?.ApplyCommission(this.Gross) ?? Gross;
    }
}