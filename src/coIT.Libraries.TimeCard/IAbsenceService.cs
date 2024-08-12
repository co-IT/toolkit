using System.Collections.Immutable;
using coIT.Libraries.TimeCard.DataContracts;

namespace coIT.Libraries.TimeCard
{
    public interface IAbsenceService
    {
        Task<IImmutableList<TimeCardAbsence>> AllAbsences(
            List<TimeCardEmployeesWithGroups> employees,
            DateTime date
        );
    }
}
