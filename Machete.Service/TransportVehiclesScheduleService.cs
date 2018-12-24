using AutoMapper;
using Machete.Data;
using Machete.Data.Infrastructure;
using Machete.Domain;

namespace Machete.Service
{
    public interface ITransportVehiclesScheduleService : IService<TransportVehicleSchedule>
    {

    }
    public class TransportVehiclesScheduleService : ServiceBase2<TransportVehicleSchedule>, ITransportVehiclesScheduleService
    {
        private readonly IMapper map;

        public TransportVehiclesScheduleService(IDatabaseFactory db, IMapper map) : base(db)
        {
            this.map = map;
            this.logPrefix = "TVS";
        }
    }
}
