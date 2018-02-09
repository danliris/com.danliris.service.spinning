using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Spinning.WebApi.Helpers;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.ViewModels;


namespace Com.Danliris.Service.Spinning.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/LotYarn")]
    public class LotYarnController : BasicController<SpinningDbContext, LotYarnService, LotYarnViewModel, LotYarn>
    {
        private static readonly string ApiVersion = "1.0";
        public LotYarnController(LotYarnService service) : base(service, ApiVersion)
        {
        }
    }
}