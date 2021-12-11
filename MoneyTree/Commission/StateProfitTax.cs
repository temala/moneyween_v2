using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace MoneyTree
{
    public class StateProfitTax : TaxBase
    {
        private readonly ICollection<TaxShare> taxThreshold;
        private readonly decimal familyCoefficient;
        
        public StateProfitTax(decimal familyCoefficient):base()
        {
            this.familyCoefficient = familyCoefficient;
            this.taxThreshold = new Collection<TaxShare>();

            this.taxThreshold.Add(new TaxShare(0, 10084, 0, 0));
            this.taxThreshold.Add(new TaxShare(10084, 25710, 0.11M, 1109.24M));
            this.taxThreshold.Add(new TaxShare(25710, 73516, 0.30M, 5994.14M));
            this.taxThreshold.Add(new TaxShare(73516, 158122, 0.41M, 14080.90M));
            this.taxThreshold.Add(new TaxShare(158122, decimal.MaxValue, 0.45M, 20405.78M));
        }

        public override decimal ApplyCommission(decimal gross)
        {
            var commission = taxThreshold.Sum(tranche => (decimal) tranche.ComputeTax(gross, familyCoefficient));
            //Console.WriteLine($"{this.GetType().Name}: {commission.ToString("c3",CultureInfo.CreateSpecificCulture("fr-FR"))}");
            this.Commissionaire.Incomes.Add(new StateCommission(commission));
            return gross-commission;
        }

        public class TaxShare
        {
            private decimal Max { get; }
            private decimal Min { get; }
            private decimal Taux { get; }

            private decimal ConstCalcule { get; }

            public TaxShare(decimal min, decimal max, decimal taux, decimal constCalcule)
            {
                Taux = taux;
                ConstCalcule = constCalcule;
                Min = min;
                Max = max;
            }

            public decimal ComputeTax(decimal salaire, decimal n)
            {
                if (Min < salaire / n && salaire / n < Max)
                {
                    return salaire * Taux - ConstCalcule * n;
                }
                return 0;
            }
        }
    }
}