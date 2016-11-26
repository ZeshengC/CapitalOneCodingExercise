using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalOneCodingExercise
{
    /// <summary>
    /// Class for calculating averages
    /// </summary>
    public class Average
    {
        public string MonthString { get; set; }
        public List<Transaction> Spent { get; set; }
        public List<Transaction> Income { get; set; }
        public bool IgnoreDonuts { get; set; }
        public bool IgnoreCCPayment { get; set; }
        public bool IsTotalAverage { get; set; }

        private List<Tuple<Transaction, Transaction>> CCPayments
        {
            get
            {
                List<Tuple<Transaction, Transaction>> ccs = new List<Tuple<Transaction, Transaction>>();
                foreach (Transaction s in Spent)
                {
                    foreach (Transaction i in Income)
                    {
                        if (s.Amount == -i.Amount && (s.TransactionTime.AddHours(-24) <= i.TransactionTime) && (i.TransactionTime <= s.TransactionTime.AddHours(24)))
                            ccs.Add(new Tuple<Transaction, Transaction>(s, i));
                    }
                }
                return ccs;
            }
        }

        public double SpentAverage
        {
            get
            {
                if(Spent != null && Spent.Count > 0)
                {
                    List<Transaction> trans = new List<Transaction>();
                    trans.AddRange(Spent);
                    if (IgnoreDonuts)
                    {
                        trans = trans.Where(t => t.Merchant != "Krispy Kreme Donuts" && t.Merchant != "Dunkin #336784").ToList();

                    }
                    if (IgnoreCCPayment)
                    {
                        foreach (Tuple<Transaction, Transaction> cc in CCPayments)
                            trans.Remove(cc.Item1);
                    }
                    double average = Math.Abs(trans.Select(t => t.Amount).Average());
                    return Math.Round(average, 2);
                }
                return 0;
                
            }
        }
        public double IncomeAverage
        {
            get
            {
                if(Income != null && Income.Count > 0)
                {
                    List<Transaction> trans = new List<Transaction>();
                    trans.AddRange(Income);
                    if (IgnoreCCPayment)
                    {
                        foreach (Tuple<Transaction, Transaction> cc in CCPayments)
                            trans.Remove(cc.Item2);
                    }
                    double average = Income.Select(t => t.Amount).Average();
                    return Math.Round(average, 2);
                }
                return 0;
                
            }
        }
        public override string ToString()
        {
            string result = string.Format("\"{0}\":{{\"spent\":\"${1}\",\"income\":\"${2}\"}}", MonthString, SpentAverage, IncomeAverage);
            if (IgnoreCCPayment && !IsTotalAverage)
            {
                StringBuilder sb = new StringBuilder(result);
                foreach (Tuple<Transaction, Transaction> cc in CCPayments)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(string.Format("\"CC Payment\":{{\"Debit\":\"${0}\",\"Credit\":\"${1}\"}}", cc.Item1.Amount, cc.Item2.Amount));
                }
                result = sb.ToString();
            }
            result = "{" + result + "}";
            return result;

        }
    }
}
