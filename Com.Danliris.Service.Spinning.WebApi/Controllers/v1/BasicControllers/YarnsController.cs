using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Spinning.WebApi.Helpers;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Com.Danliris.Service.Spinning.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/yarns")]
    [Authorize]
    public class YarnsController : BasicController<SpinningDbContext, YarnService, YarnViewModel, Yarn>
    {
        private static readonly string ApiVersion = "1.0";
        public YarnsController(YarnService service) : base(service, ApiVersion)
        {
        }
    }
}