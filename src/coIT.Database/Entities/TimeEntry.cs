using coIT.Database.Entities.ValueObjects;
using coIT.Database.Utilities;
using LiteDB;

namespace coIT.Database.Entities
{
    public class TimeEntry : Entity
    {
        [BsonRef]
        public Employee Employee { get; private set; }

        [BsonRef]
        public CustomerRelationship Customer { get; private set; }

        public DateSpan Period { get; private set; }

        public int ProjectId { get; private set; }

        public bool IsEducation { get; private set; }

        public bool IsBillable { get; private set; }

        public TimeSpan Time { get; private set; }

        [BsonRef]
        public BillingAccount BillingAccount { get; private set; }

        [BsonIgnore]
        public int ServiceNumber => BillingAccount.RevenueAccount.Number;

        public int StoreId { get; private set; }

        [BsonIgnore]
        public DateTime Date => Period.Start;

        [Obsolete("Do not use. Use parameterized constructor", true)]
        public TimeEntry() { }

        public TimeEntry(
            Employee employee,
            CustomerRelationship customer,
            DateSpan period,
            bool isBillable,
            TimeSpan time,
            int projectId,
            BillingAccount billingAccount,
            int storeId
        )
        {
            if (projectId <= 10000) //todo: improve validation
                throw new ArgumentOutOfRangeException(nameof(projectId));

            if (time.TotalSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(time));

            if (storeId <= 0)
                throw new ArgumentNullException(nameof(storeId));

            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Period = period ?? throw new ArgumentNullException(nameof(period));
            IsBillable = isBillable;
            Time = time;
            ProjectId = projectId;
            IsEducation = CheckIfProjectIsEducational();
            BillingAccount = billingAccount;
            StoreId = storeId;
        }

        private bool CheckIfProjectIsEducational()
        {
            return !IsBillable
                && (
                    ImportantClockodoIds.FurtherEducationProjectId == ProjectId
                    || ImportantClockodoIds.TrainingProjectId == ProjectId
                );
        }
    }
}
