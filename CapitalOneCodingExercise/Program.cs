using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CapitalOneCodingExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            string getAllTransactionRequest = "{\"args\": {\"uid\":  1110590645, \"token\":  \"A3C37DDA3CC09AD95A0F1D6F2C4E2DB6\", \"api-token\":  \"AppTokenForInterview\", \"json-strict-mode\": false, \"json-verbose-response\": false}}";
            string getAllTransationRequestURI = "https://2016.api.levelmoney.com/api/v2/core/get-all-transactions";

            string getProjectedTransactionsForMonthRequest = string.Format("{{ \"args\": {{ \"uid\":  1110590645, \"token\":  \"A3C37DDA3CC09AD95A0F1D6F2C4E2DB6\", \"api-token\":  \"AppTokenForInterview\", \"json-strict-mode\": false, \"json-verbose-response\": false}}, \"year\":  {0}, \"month\":  {1}}}", DateTime.Now.Year, DateTime.Now.Month);
            string getProjectedTransactionsForMonthURI = "https://2016.api.levelmoney.com/api/v2/core/projected-transactions-for-month";


            bool ignoreDonuts = args.Contains("--ignore-donuts");
            bool crystalBall = args.Contains("--crystal-ball");
            bool ignoreCCPayment = args.Contains("--ignore-cc-payments");


            var allTransactions = GetTransactions(getAllTransationRequestURI, getAllTransactionRequest);
            var monthGroup = allTransactions.GroupBy(t => t.MonthString);
            List<MonthAverage> monthAverages = monthGroup.Select(g => new MonthAverage()
            {
                MonthString = g.Key,
                Income = g.Where(t => t.Amount > 0).ToList(),
                Spent = g.Where(t => t.Amount < 0).ToList(),
                IgnoreDonuts = ignoreDonuts,
                IgnoreCCPayment = ignoreCCPayment
            }).ToList();
            if(crystalBall)
            {
                var predictedTransactions = GetTransactions(getProjectedTransactionsForMonthURI, getProjectedTransactionsForMonthRequest);
                monthAverages.Add(new MonthAverage()
                {
                    MonthString = DateTime.Now.Year + "-" + DateTime.Now.Month + " predicted average",
                    Income = predictedTransactions.Where(t => t.Amount > 0).ToList(),
                    Spent = predictedTransactions.Where(t => t.Amount < 0).ToList(),
                    IgnoreDonuts = ignoreDonuts,
                    IgnoreCCPayment = ignoreCCPayment
                });
            }
            monthAverages.Add(new MonthAverage()
            {
                MonthString = "average",
                Income = allTransactions.Where(t => t.Amount > 0).ToList(),
                Spent = allTransactions.Where(t => t.Amount < 0).ToList(),
                IgnoreDonuts = ignoreDonuts,
                IgnoreCCPayment = ignoreCCPayment
            });
            string data = string.Join(", " + Environment.NewLine, monthAverages);

            Console.Write(data);

        }

        public static List<Transaction> GetTransactions(string url, string queryString)
        {
            string result = HttpPOST(url, queryString);
            List<Transaction> transactions = new List<Transaction>();
            var jResult = JObject.Parse(result);
            var jTransactions = jResult["transactions"];
            foreach (JObject json in jTransactions)
            {
                Transaction t = new Transaction(json);
                transactions.Add(t);
            }
            return transactions;
        }
        public static string HttpPOST(string url, string queryString)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient webClient = new WebClient())
            {
                byte[] postData = Encoding.ASCII.GetBytes(queryString);
                
                webClient.Headers[HttpRequestHeader.Accept] = "*/*";
                webClient.Headers[HttpRequestHeader.AcceptLanguage] = "en-US,en;q=0.8,zh-CN;q=0.6,zh;q=0.4";
                webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                webClient.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                
                var data = webClient.UploadData(url, postData);
                byte[] decompress = Decompress(data);
                string text = System.Text.ASCIIEncoding.ASCII.GetString(decompress);
                return text;
               
            }
        }

        static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                                  CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }
}
