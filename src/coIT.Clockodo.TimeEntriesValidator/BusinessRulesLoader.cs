using coIT.Libraries.Clockodo.BusinessRules;
using Newtonsoft.Json;

namespace coIT.Clockodo.TimeEntriesValidator
{
    internal class BusinessRulesLoader
    {
        private static string _credentialsFilename = "rules.json";

        public static ClockodoBusinessRulesEvaluator LoadEvaluator()
        {
            return !ConfigurationExists()
                ? ClockodoBusinessRulesEvaluator.CreateWithDefaultCustomerToBillingAccountsAsssociationes()
                : LoadRulesFromFile();
        }

        private static bool ConfigurationExists() => File.Exists(_credentialsFilename);

        private static ClockodoBusinessRulesEvaluator LoadRulesFromFile()
        {
            var configContent = File.ReadAllText(_credentialsFilename);

            var rules = JsonConvert.DeserializeObject<BusinessRulesConfiguration>(configContent);

            return ClockodoBusinessRulesEvaluator.CreateWithCustomRuleAssociations(
                rules.ServiceRules,
                rules.TextRules
            );
        }
    }
}
