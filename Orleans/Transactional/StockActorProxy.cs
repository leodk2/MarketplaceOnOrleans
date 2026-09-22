using Common.Entities;
using Common.Events;
using OrleansApp.Interfaces;

namespace OrleansApp.Transactional;

// Forwards IStockActor calls through the correctly-typed ITransactionalStockActor
// reference, so its [Transaction]-attributed overloads (hidden via 'new') are honored
// instead of silently falling back to the untransacted base interface methods.
public sealed class TransactionalStockActorProxy : IStockActor
{
    private readonly ITransactionalStockActor grain;

    public TransactionalStockActorProxy(ITransactionalStockActor grain)
    {
        this.grain = grain;
    }

    public Task<ItemStatus> AttemptReservation(CartItem cartItem) => grain.AttemptReservation(cartItem);
    public Task CancelReservation(int quantity) => grain.CancelReservation(quantity);
    public Task ConfirmReservation(int quantity) => grain.ConfirmReservation(quantity);
    public Task ProcessProductUpdate(ProductUpdated productUpdated) => grain.ProcessProductUpdate(productUpdated);
    public Task SetItem(StockItem item) => grain.SetItem(item);
    public Task<StockItem> GetItem() => grain.GetItem();
    public Task Reset() => grain.Reset();
}
