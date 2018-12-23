using AutoMapper;
using Machete.Data;
using Machete.Data.Infrastructure;
using Machete.Domain;

namespace Machete.Service
{
    public interface ITransportProvidersAvailabilityService : IService<TransportProviderAvailability>
    {

    }
    public class TransportProvidersAvailabilityService : ServiceBase2<TransportProviderAvailability>, ITransportProvidersAvailabilityService
    {
        private readonly IMapper map;

        public TransportProvidersAvailabilityService(IDatabaseFactory db, IMapper map) : base(db)
        {
            this.map = map;
            this.logPrefix = "TransportRule";
        }
    }
}
