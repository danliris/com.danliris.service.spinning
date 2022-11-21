using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Test.Helpers;
using Models = Com.Danliris.Service.Spinning.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Spinning.Test.Services.MasterCount
{
    [Collection("ServiceProviderFixture collection")]
    public class MasterCountBasicTest : BasicServiceTest<SpinningDbContext, MasterCountService, Models.MasterCount>
    {
        private static readonly string[] createAttrAssertions = { };
        private static readonly string[] updateAttrAssertions = { };
        private static readonly string[] existAttrCriteria = { };

        public MasterCountBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        { 
        }

        public override void EmptyCreateModel(Models.MasterCount model)
        {
        }

        public override void EmptyUpdateModel(Models.MasterCount model)
        {
        }

        public override Models.MasterCount GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();
            return new Models.MasterCount()
            {
                Count = "Test",
                Remark = "Remark",
            };
        }
    }
}
