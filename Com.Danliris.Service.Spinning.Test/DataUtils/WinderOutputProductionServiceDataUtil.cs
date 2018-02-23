using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Spinning.Test.DataUtils
{
    class WinderOutputProductionServiceDataUtil
    {
        public SpinningDbContext DbContext { get; set; }

        public WinderOutputProductionService WinderOutputProductionService { get; set; }

        public WinderOutputProductionServiceDataUtil(SpinningDbContext dbContext, WinderOutputProductionService winderOutputProductionService)
        {
            this.DbContext = dbContext;
            this.WinderOutputProductionService = winderOutputProductionService;
        }

        public Task<WinderOutputProduction> GetTestWinderOutputProduction()
        {
            WinderOutputProduction testWinderOutputProduction = WinderOutputProductionService.DbSet.FirstOrDefault(winderOutputProduction => winderOutputProduction.Code == "Test");

            if (testWinderOutputProduction != null)
                return Task.FromResult(testWinderOutputProduction);
            else
            {
                testWinderOutputProduction = new WinderOutputProduction()
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
                WinderOutputProductionService.Create(testWinderOutputProduction);
                return WinderOutputProductionService.GetAsync(testWinderOutputProduction.Id);
            }
        }
    }
}
