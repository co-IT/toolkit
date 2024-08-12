using coIT.Database.Entities.ValueObjects;
using CSharpFunctionalExtensions;
using LiteDB;

namespace coIT.Database.Entities
{
    public class NominalAccount : Entity
    {
        public new int Id { get; private set; }
        public Year Year { get; private set; }
        public List<MonthlyValue> Months { get; private set; }
        public AccountName AccountName { get; private set; }
        public AccountNumber AccountNumber { get; private set; }

        [BsonRef("IncomeStatementAccount")]
        public IncomeStatementAccount IncomeStatementAccount { get; private set; }

        [BsonRef("BalanceSheetAccount")]
        public BalanceSheetAccount BalanceSheetAccount { get; private set; }

        public static string[] RepositoryInclude =>
            new[] { "$.AccountGroup", "$.IncomeStatementAccount", "$.BalanceSheetAccount" };

        public NominalAccount() { }

        public NominalAccount(
            Year year,
            List<MonthlyValue> months,
            AccountName accountName,
            AccountNumber accountNumber,
            IncomeStatementAccount incomeStatementAccount,
            BalanceSheetAccount balanceSheetAccount
        )
        {
            Year = year;
            Months = months;
            AccountName = accountName;
            AccountNumber = accountNumber;
            IncomeStatementAccount = incomeStatementAccount;
            BalanceSheetAccount = balanceSheetAccount;
        }

        public Result AssignIncomeStatementAccount(IncomeStatementAccount account)
        {
            if (account == null)
                return Result.Failure("Es wurde kein gültiges GuV Konto übergeben");

            if (account.Id == IncomeStatementAccount.Id)
                return Result.Ok();

            if (BalanceSheetAccount.Id != 1)
                return Result.Failure(
                    "Das Sachkonto ist bereits einem Bilanzkonto zugewiesen. Bitte erst das Bilanzkonto entfernen."
                );

            IncomeStatementAccount = account;
            return Result.Ok();
        }

        public Result UpdateMonthlyValues(List<MonthlyValue> newValues)
        {
            if (newValues is null || newValues.Count < 12)
                return Result.Failure("Es müssen Werte für alle 12 Monate vorhanden sein!");

            Months = newValues;
            return Result.Ok();
        }

        public Result AssignAccountNumber(int accountNumber)
        {
            try
            {
                AccountNumber = new AccountNumber(accountNumber);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }

            return Result.Ok();
        }

        public Result AssignBalanceSheetAccount(BalanceSheetAccount account)
        {
            if (account == null)
                return Result.Failure("Es wurde kein gültiges Bilanzkonto übergeben");

            if (account.Id == BalanceSheetAccount.Id)
                return Result.Ok();

            if (IncomeStatementAccount.Id != 1)
                return Result.Failure(
                    "Das Sachkonto ist bereits einem GuV Konto zugewiesen. Bitte erst das GuV Konto entfernen."
                );

            BalanceSheetAccount = account;
            return Result.Ok();
        }

        public Result AssignName(string accountName)
        {
            try
            {
                AccountName = new AccountName(accountName);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }

            return Result.Ok();
        }
    }
}
