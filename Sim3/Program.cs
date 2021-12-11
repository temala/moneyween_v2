using System;
using System.Globalization;
using MoneyTree;

namespace Sim3
{
    class Program
    {
        static void Main(string[] args)
        {
            var netsalary = 1300;
            
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    Console.WriteLine($"Salaire: {netsalary}");
                    var rtConsulting = new Company("RTConsulting");

                    var suezContract = new CompanyConsultingContract(600, 244);

                    var ridha = new Employee("Ridha");
                    var salary = new PublicSalary(netsalary, 12);

                    var rent = new EmployeeInvoice(600, 12);
                    var gas = new EmployeeInvoice(500, 12);
                    var restaurant = new EmployeeInvoice(330, 12);

                    rtConsulting.Incomes.Add(suezContract);

                    rtConsulting.Outcomes.Add(salary);
                    ridha.Incomes.Add(salary);

                    rtConsulting.Outcomes.Add(rent);
                    rtConsulting.Outcomes.Add(gas);
                    rtConsulting.Outcomes.Add(restaurant);
                    ridha.Incomes.Add(rent);
                    ridha.Incomes.Add(gas);
                    ridha.Incomes.Add(restaurant);

                    var shares = new CompanyShares(rtConsulting, 100);

                    rtConsulting.Outcomes.Add(shares);
                    ridha.Incomes.Add(shares);

                    rtConsulting.GetMoneyBalance();

                    var ridhaRevenu = ridha.GetMoneyBalance();

                    var taxOnProfit = new StateProfitTax(3);
                    var profitAmount = taxOnProfit.ApplyCommission(ridhaRevenu);

                    State.StateOfFrance().GetMoneyBalance();

                    Console.WriteLine(
                        $"profit apres taxes: {profitAmount.ToString("c3", CultureInfo.CreateSpecificCulture("fr-FR"))}");

                    Console.WriteLine("*********************");

                }
                catch (Exception e)
                {
                    break;
                }
                finally
                {
                    netsalary += 100;
                    State.StateOfFrance().Incomes.Clear();
                }

               
            }
        }
    }
}