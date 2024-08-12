using CSharpFunctionalExtensions;
using LiteDB;

namespace coIT.Database.Entities
{
    public class CustomerBillingAccountRelationship : Entity
    {
        public CustomerBillingAccountRelationship() { }

        public static Result<CustomerBillingAccountRelationship> TryCreate(
            CustomerRelationship customerRelationship,
            BillingAccount billingAccount
        )
        {
            if (customerRelationship is null || billingAccount is null)
                return Result.Failure<CustomerBillingAccountRelationship>(
                    "Kunde und Konto müssen angegeben werden"
                );

            var relation = new CustomerBillingAccountRelationship(
                customerRelationship,
                billingAccount
            );

            return Result.Success(relation);
        }

        private CustomerBillingAccountRelationship(
            CustomerRelationship customer,
            BillingAccount billingAccount
        )
        {
            Customer = customer;
            BillingAccount = billingAccount;
        }

        [BsonRef("CustomerRelationship")]
        public CustomerRelationship Customer { get; private set; }

        [BsonRef("BillingAccount")]
        public BillingAccount BillingAccount { get; private set; }

        public static string[] RepositoryInclude => new[] { "$.Customer", "$.BillingAccount" };
    }
}
