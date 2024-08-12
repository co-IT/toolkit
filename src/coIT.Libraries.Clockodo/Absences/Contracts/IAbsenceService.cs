using System.Collections.Immutable;

namespace coIT.Libraries.Clockodo.Absences.Contracts;

public interface IAbsencesService
{
    Task<IImmutableList<Absence>> AllAbsences(ClockodoPeriodFilter periodFilter);
}
