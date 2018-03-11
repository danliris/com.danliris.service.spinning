using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Services;
using Models = Com.Danliris.Service.Spinning.Lib.Models;
using System;
using Xunit;
using Com.Danliris.Service.Spinning.Test.Helpers;

namespace Com.Danliris.Service.Spinning.Test.Services.YarnOutputProduction
{
    [Collection("ServiceProviderFixture collection")]
    public class WinderOutputProductionBasicTest : BasicServiceTest<SpinningDbContext, WinderOutputProductionService, Models.WinderOutputProduction>
    {
        private static readonly string[] createAttrAssertions = { };
        private static readonly string[] updateAttrAssertions = { };
        private static readonly string[] existAttrCriteria = { };

        public WinderOutputProductionBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        public override void EmptyCreateModel(Models.WinderOutputProduction model)
        {
        }

        public override void EmptyUpdateModel(Models.WinderOutputProduction model)
        {
        }

        public override Models.WinderOutputProduction GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();
            return new Models.WinderOutputProduction()
            {
                Code = guid
            };
        }
    }
}
