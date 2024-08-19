namespace coIT.Libraries.LexOffice.DataContracts.Invoice
{
    public class InvoiceTaxAmount
    {
        public decimal TaxRatePercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
