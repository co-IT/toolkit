using System.Collections.Immutable;

namespace coIT.Libraries.Clockodo.Absences.Contracts;

public interface IEmployeesService
{
    Task<IImmutableList<EmployeeInfo>> AllEmployees();
}
