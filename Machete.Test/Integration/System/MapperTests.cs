using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Machete.Test
{
    [TestClass]
    public class MapperTests
    {
        [Ignore, TestMethod]
        public void AutoMapper_MacheteWebValidation()
        {
            var mapper = new Machete.Web.MapperConfig().getMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

        }

        [Ignore, TestMethod]
        public void AutoMapper_ApiValidation()
        {
            var mapper = new Api.MapperConfig().getMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

        }
    }
}
