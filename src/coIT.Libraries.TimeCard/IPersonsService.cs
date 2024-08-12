using System.Collections.Immutable;
using coIT.Libraries.TimeCard.DataContracts;

namespace coIT.Libraries.TimeCard
{
    public interface IPersonsService
    {
        Task<IImmutableList<TimeCardEmployeesWithGroups>> AllEmployees();
    }
}
