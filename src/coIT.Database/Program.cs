using coIT.Database;

Console.WriteLine("Beginn");
var repository = new LiteDbRepository(@"assets/ControllingWriteModel.db");

var invoices = repository.AllInvoices();

//BusinessLogic.PrintList(employees, employee => employee.Name);

invoices
    .Select(i => i.BillingAccount)
    .Where(account => account.IsSuccess)
    .Select(account => account.Value.RevenueAccount.Number)
    .Distinct()
    .OrderBy(number => number)
    .ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine();

invoices
    .Select(i => i.Debitor)
    .Select(debitor => string.Format("{0} ({1})", debitor.Name, debitor.Number))
    .Distinct()
    .OrderBy(name => name)
    .ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine();
invoices
    .SelectMany(i => i.Lines)
    .Select(l =>
        $"{l.Employee.Name.LastName}, {l.Employee.Name.FirstName} ({l.Employee.PersonnelNumber.Number})"
    )
    .Distinct()
    .OrderBy(name => name)
    .ToList()
    .ForEach(Console.WriteLine);

invoices
    .SelectMany(i => i.Lines)
    .Select(l => $"{l.Account.RevenueAccount.Number}-{l.Employee.PersonnelNumber.Number}")
    .Distinct()
    .OrderBy(name => name)
    .ToList()
    .ForEach(Console.WriteLine);

var debitornumbers = new HashSet<int>();
debitornumbers.Add(50026);
debitornumbers.Add(50030);
debitornumbers.Add(50050);

var specialGroups = invoices
    .Where(i => debitornumbers.Contains(i.Debitor.Number))
    .Where(i => i.Lines.Where(l => l.Employee.PersonnelNumber.Number >= 99998).Count() > 0)
    .ToList();

var debs = specialGroups.Select(i => i.Debitor.Number).Distinct().ToList();

foreach (var deb in debs)
{
    var invoicesOfDeb = specialGroups
        .Where(i => i.Debitor.Number == deb)
        .OrderBy(i => i.Date)
        .ToList();
    Console.WriteLine(deb + " | " + invoicesOfDeb.First().Debitor.Name);

    foreach (var inv in invoicesOfDeb)
    {
        Console.WriteLine($"\t{inv.Date.ToShortDateString()} | {inv.Number}");

        foreach (var line in inv.Lines)
        {
            Console.WriteLine(
                $"\t\t{line.Employee.Name} | {line.ProductDescription} | {line.Amount.Value:N2}"
                    ?? "\t\tkeine"
            );
        }
    }
}

decimal sumHecoGroupService = 0m;
decimal sumHecoGroupReselling = 0m;
decimal sumMarketService = 0m;
decimal sumMarketReselling = 0m;

foreach (var year in new[] { 2020, 2021, 2022 })
{
    Console.WriteLine(year);

    var rechnungenInJahr = invoices.Where(i => i.Date.Year == year);

    sumHecoGroupService = 0m;
    sumHecoGroupReselling = 0m;
    sumMarketService = 0m;
    sumMarketReselling = 0m;

    foreach (var rechnung in rechnungenInJahr)
    {
        foreach (var position in rechnung.Lines)
        {
            var posAmount = position.Amount.Value;

            if (position.IsService(rechnung.Number))
            {
                if (rechnung.IsHecoGroup())
                {
                    sumHecoGroupService += posAmount;
                }
                else
                {
                    sumMarketService += posAmount;
                }
            }
            else
            {
                if (rechnung.IsHecoGroup())
                {
                    sumHecoGroupReselling += posAmount;
                }
                else
                {
                    sumMarketReselling += posAmount;
                }
            }
        }
    }

    Console.WriteLine($"\theco-Gruppe Dienstsleistungen: {sumHecoGroupService:C2}");
    Console.WriteLine($"\tMarkt Dienstsleistungen: {sumMarketService:C2}");

    Console.WriteLine($"\theco-Gruppe Warenverkäufe: {sumHecoGroupReselling:C2}");
    Console.WriteLine($"\tMarkt Warenverkäufe: {sumMarketReselling:C2}");
}

//BusinessLogic.PrintList(invoices, invoice => invoice.Lines.First().ProductDescription);

Console.WriteLine("Fertig");
Console.ReadKey();
