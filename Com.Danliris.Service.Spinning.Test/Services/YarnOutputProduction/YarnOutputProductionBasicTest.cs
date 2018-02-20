﻿using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Services;
using Models = Com.Danliris.Service.Spinning.Lib.Models;
using System;
using Xunit;
using Com.Danliris.Service.Spinning.Test.Helpers;

namespace Com.Danliris.Service.Spinning.Test.Services.YarnOutputProduction
{
    [Collection("ServiceProviderFixture collection")]
    public class YarnOutputProductionBasicTest : BasicServiceTest<SpinningDbContext, YarnOutputProductionService, Models.YarnOutputProduction>
    {
        private static readonly string[] createAttrAssertions = { };
        private static readonly string[] updateAttrAssertions = { };
        private static readonly string[] existAttrCriteria = { };

        public YarnOutputProductionBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        public override void EmptyCreateModel(Models.YarnOutputProduction model)
        {
        }

        public override void EmptyUpdateModel(Models.YarnOutputProduction model)
        {
        }

        public override Models.YarnOutputProduction GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();
            return new Models.YarnOutputProduction()
            {
                Code = guid
            };
        }
    }
}
