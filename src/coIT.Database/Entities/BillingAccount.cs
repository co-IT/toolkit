using coIT.Database.Entities.ValueObjects;
using coIT.Database.Utilities;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class BillingAccount : Entity, IEquatable<BillingAccount>
    {
        public RevenueAccount RevenueAccount { get; private set; }
        public BusinessBranch BusinessBranch { get; private set; }
        public bool IsService { get; private set; }
        public TaxKey Type { get; private set; }
        public TaxRate TaxRate { get; private set; }
        public string InvoiceTaxNote { get; private set; }
        public bool IsValidForInvoices { get; private set; }

        public BillingAccount() { }

        private BillingAccount(
            RevenueAccount revenueAccount,
            BusinessBranch businessBranch,
            bool isService,
            TaxKey type,
            TaxRate taxRate,
            string invoiceTaxNote,
            bool isValidForInvoices
        )
        {
            RevenueAccount = revenueAccount;
            BusinessBranch = businessBranch;
            IsService = isService;
            Type = type;
            TaxRate = taxRate;
            InvoiceTaxNote = invoiceTaxNote;
            IsValidForInvoices = isValidForInvoices;
        }

        private BillingAccount(
            RevenueAccount revenueAccount,
            BusinessBranch businessBranch,
            bool isService,
            bool isSalesAccount
        )
            : this(
                revenueAccount,
                businessBranch,
                isService,
                new TaxKey(0),
                new TaxRate(0),
                string.Empty,
                isSalesAccount
            ) { }

        public static Result<BillingAccount> TryCreate(
            RevenueAccount revenueAccount,
            BusinessBranch businessBranch,
            bool isService,
            TaxKey type,
            TaxRate taxRate,
            string invoiceTaxNote,
            bool isSalesAccount
        )
        {
            if (
                invoiceTaxNote is null
                || revenueAccount is null
                || businessBranch is null
                || type is null
                || taxRate is null
            )
            {
                return Result.Failure<BillingAccount>("Alle Informationen müssen angegeben werden");
            }

            return isSalesAccount
                ? Result.Success(
                    new BillingAccount(
                        revenueAccount,
                        businessBranch,
                        isService,
                        type,
                        taxRate,
                        invoiceTaxNote,
                        true
                    )
                )
                : Result.Success(
                    new BillingAccount(revenueAccount, businessBranch, isService, false)
                );
        }

        public Result ChangeName(string name)
        {
            try
            {
                RevenueAccount = new RevenueAccount(RevenueAccount.Number, name);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeNumber(int number)
        {
            try
            {
                RevenueAccount = new RevenueAccount(number, RevenueAccount.Name);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeBusinessBranch(string businessBranch)
        {
            try
            {
                BusinessBranch = new BusinessBranch(businessBranch);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeServiceStatus(bool isService)
        {
            IsService = isService;

            return Result.Ok();
        }

        public Result ChangeSalesAccountStatus(bool isSalesAccount)
        {
            IsValidForInvoices = isSalesAccount;

            return Result.Ok();
        }

        public Result ChangeType(byte taxKey)
        {
            try
            {
                Type = new TaxKey(taxKey);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeTaxRate(double taxRate)
        {
            try
            {
                TaxRate = new TaxRate(taxRate);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result<ControllingAccount> BillingAccountToControllingAccount()
        {
            if (RevenueAccount.Number == ImportantBillingAccountNumbers.NonEuProfitsAccount)
                return Result.Success(ControllingAccount.ConsultingToMarket());

            if (RevenueAccount.Number == ImportantBillingAccountNumbers.ReverseChargeAccount)
                return Result.Success(ControllingAccount.ConsultingToMarket());

            if (RevenueAccount.Number == ImportantBillingAccountNumbers.VarinoxAccount)
                return Result.Success(ControllingAccount.ConsultingToMarket());

            if (
                RevenueAccount.Number
                == ImportantBillingAccountNumbers.MarketServiceAccountReducedTaxes
            )
                return Result.Success(ControllingAccount.ConsultingToMarket());

            if (
                RevenueAccount.Number
                == ImportantBillingAccountNumbers.MarketLicenseAccountReducedTaxes
            )
                return Result.Success(ControllingAccount.ProductSaleToMarket());

            if (RevenueAccount.Number == ImportantBillingAccountNumbers.MarketServiceAccount)
                return Result.Success(ControllingAccount.ConsultingToMarket());

            if (RevenueAccount.Number == ImportantBillingAccountNumbers.NetworkServiceAccount)
                return Result.Success(ControllingAccount.ConsultingToFiscalUnity());

            if (RevenueAccount.Number == ImportantBillingAccountNumbers.NetworkLicenseAccount)
                return Result.Success(ControllingAccount.ProductSaleToFiscalUnity());

            if (RevenueAccount.Number == ImportantBillingAccountNumbers.MarketLicenseAccount)
                return Result.Success(ControllingAccount.ProductSaleToMarket());

            if (RevenueAccount.Number == ImportantBillingAccountNumbers.InternalAccount)
                return Result.Failure<ControllingAccount>(
                    $"Dem Konto '{ImportantBillingAccountNumbers.InternalAccount}' darf kein Controlling Account zugewiesen werden."
                );

            return Result.Failure<ControllingAccount>(
                $"Zum Umsatzkonto {RevenueAccount.Number} - {RevenueAccount.Name} wurde noch kein Controlling Account zugeordnet."
            );
        }

        public Result ChangeInvoiceTaxNote(string newInvoiceTaxNote)
        {
            if (newInvoiceTaxNote is null)
                return Result.Failure("Der Steuer Hinweis muss angegeben werden");

            InvoiceTaxNote = newInvoiceTaxNote;

            return Result.Success();
        }

        public bool Equals(BillingAccount other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Equals(RevenueAccount.Number, other.RevenueAccount.Number);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((BillingAccount)obj);
        }

        public override int GetHashCode() =>
            RevenueAccount != null ? RevenueAccount.Number.GetHashCode() : 0;
    }
}
