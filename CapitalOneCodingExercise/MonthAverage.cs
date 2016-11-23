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
        public List<double> Spent { get; set; }
        public List<double> Income { get; set; }

        public override string ToString()
        {
            double spentAverage = Math.Round(Spent.Where(s => s != 0).Average(),2);
            double incomeAverage = Math.Round(Income.Where(i => i != 0).Average(),2);
            return string.Format("\"{0}\":{{\"spent\":\"${1}\",\"income\":\"${2}\"}}", MonthString, spentAverage, incomeAverage); 

        }
    }
}
