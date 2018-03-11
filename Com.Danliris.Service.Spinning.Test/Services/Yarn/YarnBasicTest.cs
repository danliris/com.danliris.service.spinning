using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Services;
using Models = Com.Danliris.Service.Spinning.Lib.Models;
using System;
using Xunit;
using Com.Danliris.Service.Spinning.Test.Helpers;

namespace Com.Danliris.Service.Spinning.Test.Service.Yarn
{
    [Collection("ServiceProviderFixture collection")]
    public class YarnBasicTest : BasicServiceTest<SpinningDbContext, YarnService, Models.Yarn>
    {
        private static readonly string[] createAttrAssertions = { };
        private static readonly string[] updateAttrAssertions = { };
        private static readonly string[] existAttrCriteria = { };

        public YarnBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        public override void EmptyCreateModel(Models.Yarn model)
        {
        }

        public override void EmptyUpdateModel(Models.Yarn model)
        {
        }

        public override Models.Yarn GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();
            return new Models.Yarn()
            {
                Code = guid,
                Name = string.Format("Test Yarn {0}", guid)
            };
        }
    }
}