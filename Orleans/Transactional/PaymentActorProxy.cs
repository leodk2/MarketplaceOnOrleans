using Common.Events;
using OrleansApp.Interfaces;

namespace OrleansApp.Transactional;

// Forwards IPaymentActor calls through the correctly-typed ITransactionalPaymentActor
// reference, so its [Transaction]-attributed overloads (hidden via 'new') are honored
// instead of silently falling back to the untransacted base interface methods.
public sealed class TransactionalPaymentActorProxy : IPaymentActor
{
    private readonly ITransactionalPaymentActor grain;

    public TransactionalPaymentActorProxy(ITransactionalPaymentActor grain)
    {
        this.grain = grain;
    }

    public Task ProcessPayment(InvoiceIssued invoiceIssued) => grain.ProcessPayment(invoiceIssued);
}
