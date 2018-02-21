using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Spinning.WebApi.Helpers;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Com.Danliris.Service.Spinning.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/LotYarn")]
    [Authorize]
    public class LotYarnController : BasicController<SpinningDbContext, LotYarnService, LotYarnViewModel, LotYarn>
    {
        private static readonly string ApiVersion = "1.0";
        public LotYarnController(LotYarnService service) : base(service, ApiVersion)
        {
        }

        [HttpGet("{Spinning}/{Machine}/{Yarn}")]
        public async Task<IActionResult> GetByQuery([FromRoute] string Spinning, [FromRoute] string Machine, [FromRoute] string Yarn)
        {
            var model = await Service.ReadModelByQuery(Spinning, Machine, Yarn);

            try
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok(model, Service.MapToViewModel);
                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}