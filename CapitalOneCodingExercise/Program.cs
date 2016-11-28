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
            try
            {
                bool ignoreDonuts = false;
                bool crystalBall = false;
                bool ignoreCCPayment = false;

                foreach (string arg in args)
                {
                    if (arg != "--ignore-donuts" && arg != "--crystal-ball" && arg != "--ignore-cc-payments")
                        throw new Exception("Invalid parameter: " + arg);
                    if (arg == "--ignore-donuts")
                        ignoreDonuts = true;
                    if (arg == "--crystal-ball")
                        crystalBall = true;
                    if (arg == "--ignore-cc-payments")
                        ignoreCCPayment = true;
                }

                if (args.Length > 0 && !(ignoreCCPayment || crystalBall || ignoreCCPayment))
                    throw new Exception("Invalid parameter");

                string getAllTransactionRequest = ConfigLookups.AllTransactionRequest;
                string getAllTransationRequestURI = ConfigLookups.AllTransactionURI;

                // Get all transactions and group them by the month
                var allTransactions = GetTransactions(getAllTransationRequestURI, getAllTransactionRequest);
                var monthGroup = allTransactions.GroupBy(t => t.MonthString);
                List<Average> monthAverages = monthGroup.Select(g => new Average()
                {
                    MonthString = g.Key,
                    Income = g.Where(t => t.Amount > 0).ToList(),
                    Spent = g.Where(t => t.Amount < 0).ToList(),
                    IgnoreDonuts = ignoreDonuts,
                    IgnoreCCPayment = ignoreCCPayment
                }).ToList();

                // Add predicted average
                if (crystalBall)
                {
                    string getProjectedTransactionsForMonthRequest = string.Format(ConfigLookups.ProjectedTransactionRequest, DateTime.Now.Year, DateTime.Now.Month);
                    string getProjectedTransactionsForMonthURI = ConfigLookups.ProjectedTransactionURI;
                    var predictedTransactions = GetTransactions(getProjectedTransactionsForMonthURI, getProjectedTransactionsForMonthRequest);
                    monthAverages.Add(new Average()
                    {
                        MonthString = DateTime.Now.Year + "-" + DateTime.Now.Month + " predicted average",
                        Income = predictedTransactions.Where(t => t.Amount > 0).ToList(),
                        Spent = predictedTransactions.Where(t => t.Amount < 0).ToList(),
                        IgnoreDonuts = ignoreDonuts,
                        IgnoreCCPayment = ignoreCCPayment
                    });
                }

                // Add the average of all transactions
                monthAverages.Add(new Average()
                {
                    MonthString = "average",
                    Income = allTransactions.Where(t => t.Amount > 0).ToList(),
                    Spent = allTransactions.Where(t => t.Amount < 0).ToList(),
                    IgnoreDonuts = ignoreDonuts,
                    IgnoreCCPayment = ignoreCCPayment,
                    IsTotalAverage = true
                });

                string data = string.Join(", " + Environment.NewLine, monthAverages);

                Console.Write(data);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private static List<Transaction> GetTransactions(string url, string queryString)
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
        private static string HttpPOST(string url, string queryString)
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

        private static byte[] Decompress(byte[] gzip)
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
