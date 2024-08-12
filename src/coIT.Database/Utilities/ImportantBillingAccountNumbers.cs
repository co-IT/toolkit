using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coIT.Database.Utilities
{
    internal static class ImportantBillingAccountNumbers
    {
        public static int NonEuProfitsAccount => 8102;
        public static int ReverseChargeAccount => 8125;
        public static int VarinoxAccount => 8337;
        public static int MarketServiceAccountReducedTaxes => 8400;
        public static int MarketLicenseAccountReducedTaxes => 8406;
        public static int MarketServiceAccount => 8500;
        public static int NetworkServiceAccount => 8504;
        public static int NetworkLicenseAccount => 8505;
        public static int MarketLicenseAccount => 8506;
        public static int InternalAccount => 9999;
    }
}
