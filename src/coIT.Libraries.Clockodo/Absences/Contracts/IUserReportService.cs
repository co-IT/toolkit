using System.Collections.Immutable;

namespace coIT.Libraries.Clockodo.Absences.Contracts;

public interface IUserReportService
{
    Task<IImmutableList<UserReport>> AllUserReports(ClockodoPeriodFilter periodFilter);
}
