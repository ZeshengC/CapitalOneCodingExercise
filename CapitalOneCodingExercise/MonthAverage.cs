using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalOneCodingExercise
{
    public class MonthAverage
    {
        public string MonthString { get; set; }
        public List<Transaction> Spent { get; set; }
        public List<Transaction> Income { get; set; }
        public bool IgnoreDonuts { get; set; }
        public bool CrystalBall { get; set; }
        public bool IgnoreCCPayment { get; set; }
        public double SpentAverage
        {
            get
            {
                List<Transaction> trans = new List<Transaction>();
                trans.AddRange(Spent);
                if(IgnoreDonuts)
                {
                    trans = trans.Where(t => t.Merchant != "Krispy Kreme Donuts" && t.Merchant != "DUNKIN #336784").ToList();
                    
                }
                double average = Math.Abs(trans.Select(t => t.Amount).Average());
                return Math.Round(average, 2);
            }
        }
        public double IncomeAverage
        {
            get
            {
                double average = Income.Select(t => t.Amount).Average();
                return Math.Round(average, 2);
            }
        }
        public override string ToString()
        {
            return string.Format("\"{0}\":{{\"spent\":\"${1}\",\"income\":\"${2}\"}}", MonthString, SpentAverage, IncomeAverage); 

        }
    }
}
