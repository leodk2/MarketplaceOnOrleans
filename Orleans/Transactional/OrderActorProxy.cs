using Common.Entities;
using Common.Events;
using OrleansApp.Interfaces;

namespace OrleansApp.Transactional;

// Forwards IOrderActor calls through the correctly-typed ITransactionalOrderActor
// reference, so its [Transaction]-attributed overloads (hidden via 'new') are honored
// instead of silently falling back to the untransacted base interface methods.
public sealed class TransactionalOrderActorProxy : IOrderActor
{
    private readonly ITransactionalOrderActor grain;

    public TransactionalOrderActorProxy(ITransactionalOrderActor grain)
    {
        this.grain = grain;
    }

    public Task Checkout(ReserveStock reserveStock) => grain.Checkout(reserveStock);
    public Task ProcessPaymentConfirmed(PaymentConfirmed paymentConfirmed) => grain.ProcessPaymentConfirmed(paymentConfirmed);
    public Task ProcessPaymentFailed(PaymentFailed paymentFailed) => grain.ProcessPaymentFailed(paymentFailed);
    public Task ProcessShipmentNotification(ShipmentNotification shipmentNotification) => grain.ProcessShipmentNotification(shipmentNotification);
    public Task<List<Order>> GetOrders() => grain.GetOrders();
    public Task<int> GetNumOrders() => grain.GetNumOrders();
    public Task Reset() => grain.Reset();
}
