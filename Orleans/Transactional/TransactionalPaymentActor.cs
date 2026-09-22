using Common.Config;
using Microsoft.Extensions.Logging;
using OrleansApp.Abstract;
using OrleansApp.Grains;
using OrleansApp.Infra;
using OrleansApp.Interfaces;

namespace OrleansApp.Transactional;

/**
* For some unknown reason, having reentrancy here leads to non-deterministic degradation of performance.
*/
public sealed class TransactionalPaymentActor : AbstractPaymentActor, ITransactionalPaymentActor
{
    public TransactionalPaymentActor(IAuditLogger persistence, AppConfig options, ILogger<PaymentActor> _logger) : base(persistence, options, _logger)
    {
    }

    protected override IOrderActor GetOrderActor(int id)
    {
        return new TransactionalOrderActorProxy(GrainFactory.GetGrain<ITransactionalOrderActor>(id));
    }

    protected override IShipmentActor GetShipmentActor(int id)
    {
        return new TransactionalShipmentActorProxy(GrainFactory.GetGrain<ITransactionalShipmentActor>(id));
    }

    protected override IStockActor GetStockActor(int sellerId, string productId)
    {
        return new TransactionalStockActorProxy(GrainFactory.GetGrain<ITransactionalStockActor>(sellerId, productId));
    }
}

