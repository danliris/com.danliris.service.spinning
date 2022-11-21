using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Spinning.Test.DataUtils
{
    public class MasterCountServiceDataUtil
    {
        public SpinningDbContext DbContext { get; set; }

        public MasterCountService MasterCountService { get; set; }

        public MasterCountServiceDataUtil(SpinningDbContext dbContext, MasterCountService masterCountService)
        {
            this.DbContext = dbContext;
            this.MasterCountService = masterCountService;
        }

        public Task<MasterCount> GetTestMasterCount()
        {
            MasterCount testMasterCount = MasterCountService.DbSet.FirstOrDefault(masterCount => masterCount.Count == "Test");

            if (testMasterCount != null)
            {
                return Task.FromResult(testMasterCount);
            }
            else
            {
                testMasterCount = new MasterCount()
                {
                    Count = "Test",
                    Remark = "Remark",
                };

                MasterCountService.Create(testMasterCount);
                return MasterCountService.GetAsync(testMasterCount.Id);
            }

        }
    }
}
