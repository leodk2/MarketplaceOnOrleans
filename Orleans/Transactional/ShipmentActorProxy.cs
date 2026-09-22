using Common.Entities;
using Common.Events;
using OrleansApp.Interfaces;

namespace OrleansApp.Transactional;

// Forwards IShipmentActor calls through the correctly-typed ITransactionalShipmentActor
// reference, so its [Transaction]-attributed overloads (hidden via 'new') are honored
// instead of silently falling back to the untransacted base interface methods.
public sealed class TransactionalShipmentActorProxy : IShipmentActor
{
    private readonly ITransactionalShipmentActor grain;

    public TransactionalShipmentActorProxy(ITransactionalShipmentActor grain)
    {
        this.grain = grain;
    }

    public Task<List<Shipment>> GetShipments(int customerId) => grain.GetShipments(customerId);
    public Task ProcessShipment(PaymentConfirmed paymentConfirmed) => grain.ProcessShipment(paymentConfirmed);
    public Task UpdateShipment(string tid) => grain.UpdateShipment(tid);
    public Task UpdateShipment(string tid, ISet<(int customerId, int orderId, int sellerId)> entries) => grain.UpdateShipment(tid, entries);
    public Task Reset() => grain.Reset();
}
