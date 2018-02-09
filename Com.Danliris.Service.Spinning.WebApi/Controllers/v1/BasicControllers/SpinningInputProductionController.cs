using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using Com.Danliris.Service.Spinning.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Spinning.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/SpinningInputProduction")]
    public class SpinningInputProductionController : BasicController<SpinningDbContext, SpinningInputProductionService, SpinningInputProductionViewModel, SpinningInputProduction>
    {
        private static readonly string ApiVersion = "1.0";
        public SpinningInputProductionController(SpinningInputProductionService service) : base(service, ApiVersion)
        {
        }
    }
}
