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
        public List<int> Spent { get; set; }
        public List<int> Income { get; set; }

        public override string ToString()
        {
            string spentAverage = Spent.Average().ToString("#.##");
            string incomeAverage = Income.Average().ToString("#.##");
            return string.Format("\"{0}\":{\"spent\":\"${1}\",\"income\":\"${2}\"}", MonthString, spentAverage, incomeAverage); 

        }
    }
}
