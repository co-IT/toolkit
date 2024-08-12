namespace coIT.Libraries.TimeCard.DataContracts;

public class TimeCardEmployeesWithGroups
{
    internal TimeCardEmployeesWithGroups(TimeCardEmployee employee, List<int> groups)
    {
        Id = employee.Id;
        ParentId = employee.ParentId;
        State = employee.State;
        PersonNo = employee.PersonNo;
        PersonNoString = employee.PersonNoString;
        Firstname = employee.Firstname;
        Lastname = employee.Lastname;
        Name = employee.Name;
        Department = employee.Department;
        HasNoSupervisor = employee.HasNoSupervisor;
        Roles = employee.Roles;
        Groups = groups;
    }

    public int Id { get; }

    public int ParentId { get; }

    public int State { get; }

    public int PersonNo { get; }

    public string? PersonNoString { get; }

    public string? Firstname { get; }

    public string? Lastname { get; }

    public string? Name { get; }

    public string? Department { get; }

    public bool HasNoSupervisor { get; }

    public int Roles { get; }
    public List<int> Groups { get; }
}
