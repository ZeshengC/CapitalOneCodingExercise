using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalOneCodingExercise
{
    public static class ConfigLookups
    {
        public static readonly string AllTransactionRequest;
        public static readonly string AllTransactionURI;
        public static readonly string ProjectedTransactionRequest;
        public static readonly string ProjectedTransactionURI;

        static ConfigLookups()
        {
            var appSettings = ConfigurationManager.AppSettings;
            try
            {
                AllTransactionRequest = appSettings["AllTransactionRequest"];
                AllTransactionURI = appSettings["AllTransactionURI"];
                ProjectedTransactionRequest = appSettings["ProjectedTransactionRequest"];
                ProjectedTransactionURI = appSettings["ProjectedTransactionURI"];
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
