using System.Collections.Immutable;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using coIT.Libraries.LexOffice.DataContracts.Voucher;

namespace coIT.Libraries.LexOffice;

public interface IInvoiceService
{
    Task<IImmutableList<Voucher>> GetAllInvoiceVouchersAsync(CancellationToken cancellationToken);

    Task<IImmutableList<Invoice>> GetInvoicesAsync(
        IImmutableList<Voucher> vouchers,
        CancellationToken cancellationToken
    );
}
