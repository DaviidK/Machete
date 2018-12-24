using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machete.Test.Integration.Services
{
    [TestClass]
    public class TransportSchedulingTests
    {
        FluentRecordBase frb;

        [ClassInitialize]
        public static void ClassInitialize(TestContext c) { }

        [TestInitialize]
        public void TestInitialize()
        {
            frb = new FluentRecordBase();
        }

        [TestMethod, TestCategory(TC.IT), TestCategory(TC.Service), TestCategory(TC.OnlineOrders)]
        public void CreateVehicleSchedule_Seceeds()
        {
            var tvs = frb.ToTransportVehicleSchedule();

            Assert.IsNotNull(tvs);
        }
    }
}
