using coIT.Database.Entities.ValueObjects;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class CustomerRelationship : Entity
    {
        public Recipient Recipient { get; private set; }
        public Debitor Debitor { get; private set; }
        public Address Address { get; private set; }

        public TypeOfCustomer Type { get; private set; }

        public CustomerRelationship() { }

        private CustomerRelationship(
            Recipient recipient,
            Debitor debitor,
            Address address,
            TypeOfCustomer type
        )
        {
            Recipient = recipient;
            Debitor = debitor;
            Address = address;
            Type = type;
        }

        public static Result<CustomerRelationship> TryCreate(
            Recipient recipient,
            Debitor debitor,
            Address address,
            TypeOfCustomer type
        )
        {
            if (recipient is null)
                return Result.Failure<CustomerRelationship>(
                    "Leistungsempfänger muss angegeben werden"
                );

            if (debitor is null)
                return Result.Failure<CustomerRelationship>("Debitor muss angegeben werden");

            if (address is null)
                return Result.Failure<CustomerRelationship>("Adresse muss angegeben werden");

            return Result.Success(new CustomerRelationship(recipient, debitor, address, type));
        }

        public Result ChangeCustomerName(string name)
        {
            try
            {
                Recipient = new Recipient(name, Recipient.Number);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeCustomerNumber(int number)
        {
            try
            {
                Recipient = new Recipient(Recipient.Name, number);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeDebitorNumber(int number)
        {
            try
            {
                Debitor = new Debitor(Debitor.Name, number);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeDebitorName(string name)
        {
            try
            {
                Debitor = new Debitor(name, Debitor.Number);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result ChangeAddress(string zipCode, string city, string street, string country)
        {
            try
            {
                Address = new Address(street, zipCode, country, city);

                return Result.Ok();
            }
            catch (ArgumentException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public static int OneTimeCustomer => 53029;
    }
}
