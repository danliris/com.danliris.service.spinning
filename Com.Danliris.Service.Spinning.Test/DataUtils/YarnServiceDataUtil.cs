using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Spinning.Test.DataUtils
{
    public class YarnServiceDataUtil
    {
        public SpinningDbContext DbContext { get; set; }

        public YarnService YarnService { get; set; }

        public YarnServiceDataUtil(SpinningDbContext dbContext, YarnService yarnService)
        {
            this.DbContext = dbContext;
            this.YarnService = yarnService;
        }

        public Task<Yarn> GetTestYarn()
        {
            Yarn testYarn = YarnService.DbSet.FirstOrDefault(yarn => yarn.Code == "Test");

            if (testYarn != null)
                return Task.FromResult(testYarn);
            else
            {
                testYarn = new Yarn()
                {
                    Code = "Test",
                    Name = "Test Yarn"
                };
                YarnService.Create(testYarn);
                return YarnService.GetAsync(testYarn.Id);
            }
        }
    }
}
