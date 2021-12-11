using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualBasic;
using OptimisationTax;

namespace OptimisationTax
{
    public class TaxOnProfit : ITax
    {
        private ICollection<TaxShare> tranchesImpot;
        private IHumanResource person;
        private decimal FamilyCoefficient { get;}

        public TaxOnProfit(IHumanResource person, decimal familyCoefficient)
        {
            this.person = person;
            this.FamilyCoefficient = familyCoefficient;
            this.tranchesImpot = new Collection<TaxShare>();

            this.tranchesImpot.Add(new TaxShare(0, 10084, 0, 0));
            this.tranchesImpot.Add(new TaxShare(10084, 25710, 0.11M, 1109.24M));
            this.tranchesImpot.Add(new TaxShare(25710, 73516, 0.30M, 5994.14M));
            this.tranchesImpot.Add(new TaxShare(73516, 158122, 0.41M, 14080.90M));
            this.tranchesImpot.Add(new TaxShare(158122, decimal.MaxValue, 0.45M, 20405.78M));
        }

        public decimal GetTotalCost(int workingDays)
        {
            var profit = this.person.GetRevenu(workingDays);
            return tranchesImpot.Sum(tranche => (decimal) tranche.ComputeTax(profit, FamilyCoefficient));
        }
    }

    internal class TaxShare
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