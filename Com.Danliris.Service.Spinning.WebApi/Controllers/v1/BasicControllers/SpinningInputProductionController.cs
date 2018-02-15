using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using Com.Danliris.Service.Spinning.WebApi.Helpers;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [HttpPost("create")]
        public async Task<IActionResult> CreateOutput([FromBody] SpinningInputProductionViewModel ViewModel)
        {
            try
            {
                this.Validate(ViewModel);
                List<SpinningInputProduction> models = new List<SpinningInputProduction>();
                foreach (SpinningInputProductionViewModel.Input data in ViewModel.input)
                {
                    SpinningInputProduction model = new SpinningInputProduction();
                    model.Date = (DateTime)ViewModel.Date;
                    model.Shift = ViewModel.Shift;
                    model.YarnId = ViewModel.Yarn.Id;
                    model.YarnName = ViewModel.Yarn.Name;
                    model.UnitId = ViewModel.Unit._id;
                    model.UnitName = ViewModel.Unit.name;
                    model.MachineId = ViewModel.Machine._id;
                    model.MachineName = ViewModel.Machine.name;
                    model.Lot = ViewModel.Lot;

                    model.Counter = (double)data.Counter;
                    model.Hank = (double)data.Hank;

                    models.Add(model);
                }

                await Service.CreateModels(models);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.CREATED_STATUS_CODE, General.OK_MESSAGE)
                    .Ok();
                return Created(String.Concat(HttpContext.Request.Path, "/", models[0].Id), Result);
            }
            catch (ServiceValidationExeption e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        private void Validate(SpinningInputProductionViewModel viewModel)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(viewModel, this.Service.ServiceProvider, null);

            if (!Validator.TryValidateObject(viewModel, validationContext, validationResults, true))
                throw new ServiceValidationExeption(validationContext, validationResults);
        }

    }
}
