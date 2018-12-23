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
    public interface ITransportRuleService : IService<TransportRule>
    {

    }
    public class TransportRuleService : ServiceBase2<TransportRule>, ITransportRuleService
    {
        private readonly IMapper map;

        public TransportRuleService(IDatabaseFactory db, IMapper map) : base(db)
        {
            this.map = map;
            this.logPrefix = "TransportRule";
        }
    }
}
