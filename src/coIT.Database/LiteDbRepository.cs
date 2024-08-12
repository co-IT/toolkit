using coIT.Database.Entities;
using LiteDB;

namespace coIT.Database;

public class LiteDbRepository
{
    private readonly string _databaseFile;

    public LiteDbRepository(string databaseFile)
    {
        _databaseFile = databaseFile;
    }

    public List<Employee> AllEmployees()
    {
        using var db = new LiteDatabase("assets/ControllingWriteModel.db");
        var employees = db.GetCollection<Employee>("Employee").Query().ToList();

        return employees;
    }

    public List<Invoice> AllInvoices()
    {
        using var db = new LiteDatabase("assets/ControllingWriteModel.db");

        var invoices = db.GetCollection<Invoice>()
            .Include(BsonExpression.Create("$.Lines[*]"))
            .Include(BsonExpression.Create("$.Lines[*].Employee"))
            .Include(BsonExpression.Create("$.Lines[*].Account"))
            .Include(BsonExpression.Create("$.Debitor[*]"))
            .FindAll()
            .ToList()
            .Where(invoice => invoice.Date > new DateTime(2019, 12, 31, 23, 55, 0))
            .OrderBy(invoice => invoice.Date)
            .ToList();

        return invoices;
    }

    public List<T> List<T>()
    {
        var entityName = typeof(T).Name;

        using var db = new LiteDatabase("assets/ControllingWriteModel.db");

        var entities = db.GetCollection<T>(entityName).Query().ToList();

        return entities;
    }
}
