using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Spinning.Test.DataUtils
{
    class YarnOutputProductionServiceDataUtil
    {
        public SpinningDbContext DbContext { get; set; }

        public YarnOutputProductionService YarnOutputProductionService { get; set; }

        public YarnOutputProductionServiceDataUtil(SpinningDbContext dbContext, YarnOutputProductionService yarnOutputProductionService)
        {
            this.DbContext = dbContext;
            this.YarnOutputProductionService = yarnOutputProductionService;
        }

        public Task<YarnOutputProduction> GetTestYarnOutputProduction()
        {
            YarnOutputProduction testYarnOutputProduction = YarnOutputProductionService.DbSet.FirstOrDefault(yarnOutputProduction => yarnOutputProduction.Code == "Test");

            if (testYarnOutputProduction != null)
                return Task.FromResult(testYarnOutputProduction);
            else
            {
                testYarnOutputProduction = new YarnOutputProduction()
                {
                    Code = "Test",
                    Date = DateTime.Now,
                    BadOutput = 0,
                    GoodOutput = 1,
                    DrumTotal = 1,
                    LotYarnCode = "Test",
                    LotYarnId = 1,
                    LotYarnName = "Test",
                    MachineId = "test",
                    MachineName = "Test",
                    Shift = "Test",
                    SpinningName = "Tetst",
                    SpinningId = "Test",
                    YarnCode = "Test",
                    YarnId = 1,
                    YarnName = "Test",
                    YarnWeightPerCone = 1
                };
                YarnOutputProductionService.Create(testYarnOutputProduction);
                return YarnOutputProductionService.GetAsync(testYarnOutputProduction.Id);
            }
        }
    }
}
