﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalOneCodingExercise
{
    public class Transaction
    {
        public Transaction(JObject json)
        {
            Amount = (double)json["amount"];
            IsPending = (bool)json["is-pending"];
            AggregationTime = (long)json["aggregation-time"];
            AccountId = (string)json["account-id"];
            ClearDate = (long)json["clear-date"];
            TransactionId = (string)json["clear-date"];
            RawMerchant = (string)json["raw-merchant"];
            Categorization = (string)json["categorization"];
            Merchant = (string)json["merchant"];
            TransactionTime = (DateTime)json["transaction-time"];
            MonthString = TransactionTime.Year + "-" + TransactionTime.Month;
            if(Amount < 0)
            {
                Spent = Math.Abs(Amount);
            }
            else
            {
                Income = Amount;
            }

        }
        public double Amount { get; set; }
        public bool IsPending { get; set; }
        public long AggregationTime { get; set; }
        public string AccountId { get; set; }
        public long ClearDate { get; set; }
        public string TransactionId { get; set; }
        public string RawMerchant { get; set; }
        public string Categorization { get; set; }
        public string Merchant { get; set; }
        public DateTime TransactionTime { get; set; }
        public string MonthString { get; set; }
        public double Spent { get; set; }
        public double Income { get; set; }

    }
}