namespace coIT.Libraries.LexOffice.Filter.CustomerEmployeeRevenueData;

public interface IFilterCustomerEmployeeRevenueData
{
    IEnumerable<DataContracts.CustomerEmployeeRevenueData> Filter(
        IEnumerable<DataContracts.CustomerEmployeeRevenueData> source
    );
}
