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
        public bool IgnoreCCPayment { get; set; }
        public Tuple<Transaction,Transaction> CCPayment
        {
            get
            {
                foreach(Transaction s in Spent)
                {
                    foreach(Transaction i in Income)
                    {
                        if (s.Amount == -i.Amount && (s.TransactionTime.AddHours(-24) <= i.TransactionTime) && (i.TransactionTime <= s.TransactionTime.AddHours(24)))
                            return new Tuple<Transaction, Transaction>(s, i);
                    }
                }
                return null;
            }
        }

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
                if(IgnoreCCPayment)
                {
                    if(CCPayment != null)
                        trans.Remove(CCPayment.Item1);
                }
                double average = Math.Abs(trans.Select(t => t.Amount).Average());
                return Math.Round(average, 2);
            }
        }
        public double IncomeAverage
        {
            get
            {
                List<Transaction> trans = new List<Transaction>();
                trans.AddRange(Income);
                if (IgnoreCCPayment)
                {
                    if(CCPayment != null)
                        trans.Remove(CCPayment.Item2);
                }
                double average = Income.Select(t => t.Amount).Average();
                return Math.Round(average, 2);
            }
        }
        public override string ToString()
        {
            string result = string.Format("\"{0}\":{{\"spent\":\"${1}\",\"income\":\"${2}\"}}", MonthString, SpentAverage, IncomeAverage);
            if(IgnoreCCPayment)
            {
                if(CCPayment != null)
                {
                    StringBuilder sb = new StringBuilder(result);
                    sb.Append(Environment.NewLine);
                    sb.Append(string.Format("\"CC Payment\":{{\"Credit Transaction\":\"${0}\",\"Debit Transaction\":\"${1}\"}}",CCPayment.Item2.Amount, CCPayment.Item1.Amount));
                    result = sb.ToString();
                }
            }
            return result;

        }
    }
}
