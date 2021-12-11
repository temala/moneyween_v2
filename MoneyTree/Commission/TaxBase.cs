using System;
using System.Globalization;

namespace MoneyTree
{
    public abstract class TaxBase : IMoneyFlowCommissionaire
    {
        protected decimal Taux;

        protected TaxBase()
        {
            Commissionaire = State.StateOfFrance();
        }

        public IMoneyTreeNode Commissionaire { get; }

        public virtual decimal ApplyCommission(decimal gross)
        {
            var commission = gross * Taux;
            //Console.WriteLine($"{this.GetType().Name}: {commission.ToString("c3",CultureInfo.CreateSpecificCulture("fr-FR"))}");
            
            this.Commissionaire.Incomes.Add(new StateCommission(commission));
            return gross - commission;
        }
    }
}