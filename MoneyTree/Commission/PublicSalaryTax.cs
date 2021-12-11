using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MoneyTree
{
    public class PublicSalaryTax : TaxBase
    {
        private readonly ICollection<TaxShare> taxThreshold;
        public PublicSalaryTax():base()
        {

            taxThreshold = new List<TaxShare>();
            
            this.taxThreshold.Add(new TaxShare(0, 1480, 0.14M));
            this.taxThreshold.Add(new TaxShare(1480, 1500, 0.15M));
            this.taxThreshold.Add(new TaxShare(1500, 1600, 0.19M));
            this.taxThreshold.Add(new TaxShare(1600, 1700, 0.23M));
            this.taxThreshold.Add(new TaxShare(1700, 1800, 0.27M));
            this.taxThreshold.Add(new TaxShare(1800, 1900, 0.30M));
            this.taxThreshold.Add(new TaxShare(1900, 2000, 0.33M));
            this.taxThreshold.Add(new TaxShare(2000, 2100, 0.36M));
            this.taxThreshold.Add(new TaxShare(2100, 2200, 0.36M));
            this.taxThreshold.Add(new TaxShare(2200, 2300, 0.38M));
            this.taxThreshold.Add(new TaxShare(2300, 2500, 0.41M));
            this.taxThreshold.Add(new TaxShare(2500, 3000, 0.42M));
            this.taxThreshold.Add(new TaxShare(3000, 4000, 0.42M));
            this.taxThreshold.Add(new TaxShare(4000, 5000, 0.42M));
            this.taxThreshold.Add(new TaxShare(5000, decimal.MaxValue, 0.42M));
        }
        
        public override decimal ApplyCommission(decimal gross)
        {
            var commission = taxThreshold.Sum(tranche => (decimal) tranche.ComputeTax(gross));
            //Console.WriteLine($"{this.GetType().Name}: {commission.ToString("c3",CultureInfo.CreateSpecificCulture("fr-FR"))}");

            return gross+commission;    
        }
        
        public class TaxShare
        {
            private decimal Max { get; }
            private decimal Min { get; }
            private decimal Taux { get; }


            public TaxShare(decimal min, decimal max, decimal taux)
            {
                Taux = taux;
                Min = min;
                Max = max;
            }

            public decimal ComputeTax(decimal salaire)
            {
                if (Min < salaire  && salaire  < Max)
                {
                    return salaire * Taux ; 
                }
                return 0;
            }
        }
    }
}