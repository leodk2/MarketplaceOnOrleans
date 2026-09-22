using Common.Entities;
using Common.Requests;
using OrleansApp.Interfaces;

namespace OrleansApp.Transactional;

// Forwards IProductActor calls through the correctly-typed ITransactionalProductActor
// reference, so its [Transaction]-attributed overloads (hidden via 'new') are honored
// instead of silently falling back to the untransacted base interface methods.
public sealed class TransactionalProductActorProxy : IProductActor
{
    private readonly ITransactionalProductActor grain;

    public TransactionalProductActorProxy(ITransactionalProductActor grain)
    {
        this.grain = grain;
    }

    public Task SetProduct(Product product) => grain.SetProduct(product);
    public Task<Product> GetProduct() => grain.GetProduct();
    public Task ProcessProductUpdate(Product product) => grain.ProcessProductUpdate(product);
    public Task ProcessPriceUpdate(PriceUpdate priceUpdate) => grain.ProcessPriceUpdate(priceUpdate);
    public Task Reset() => grain.Reset();
}
