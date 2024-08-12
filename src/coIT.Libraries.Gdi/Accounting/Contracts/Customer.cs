namespace coIT.Libraries.Gdi.Accounting.Contracts;

public class Customer
{
    public string Name { get; set; }
    public int Number { get; set; }
    public Address Address { get; set; }
    public CustomerType Type { get; set; }

    public static int OneTimeCustomerNumber => 53029;
}
