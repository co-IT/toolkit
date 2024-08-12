using coIT.Database.Entities.ValueObjects;
using CSharpFunctionalExtensions;
using LiteDB;

namespace coIT.Database.Entities
{
    public class PersonnelBillableDaysInfo : Entity
    {
        [BsonRef("Employee")]
        public Employee Employee { get; private set; }

        public Year Year { get; private set; }
        public Month Month { get; private set; }
        public BillableDays BillableDays { get; private set; }

        public PersonnelBillableDaysInfo() { }

        public Result ChangeBillableDays(double days)
        {
            try
            {
                BillableDays = new BillableDays(days);

                return Result.Ok();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
