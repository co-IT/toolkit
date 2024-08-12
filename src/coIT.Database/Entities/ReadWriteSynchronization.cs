namespace coIT.Database.Entities
{
    public class ReadWriteSynchronization : Entity
    {
        public DateTime LastWrite { get; private set; }
        public DateTime LastSync { get; private set; }

        public ReadWriteSynchronization() { }

        public ReadWriteSynchronization(int id, DateTime lastWrite, DateTime lastSync)
        {
            Id = id;

            if (default == lastWrite)
                throw new ArgumentOutOfRangeException(nameof(lastWrite), "Schreibdatum ungültig");

            if (default == lastSync)
                throw new ArgumentOutOfRangeException(nameof(lastSync), "Syncdatum ungültig");

            LastWrite = lastWrite;
            LastSync = lastSync;
        }

        public bool IsSyncRequired() => !(LastWrite.Subtract(LastSync).TotalSeconds < 1);

        public static ReadWriteSynchronization CreateRequiredSync() =>
            new ReadWriteSynchronization(1, DateTime.Now, DateTime.Now.AddSeconds(-5));

        public void SyncedSuccessfully() => LastSync = DateTime.Now;

        public void ChangesMade() => LastWrite = DateTime.Now;
    }
}
