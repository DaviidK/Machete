using AutoMapper;
using Machete.Data;
using Machete.Data.Infrastructure;
using Machete.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machete.Service
{
    public interface ITransportCostRuleService : IService<TransportCostRule>
    {

    }
    public class TransportCostRuleService : ServiceBase2<TransportCostRule>, ITransportCostRuleService
    {
        private readonly IMapper map;

        public TransportCostRuleService(IDatabaseFactory db, IMapper map) : base(db)
        {
            this.map = map;
            this.logPrefix = "TransportCostRule";
        }
    }
}
