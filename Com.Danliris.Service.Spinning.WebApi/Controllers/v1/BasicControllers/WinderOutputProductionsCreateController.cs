using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Spinning.WebApi.Helpers;
using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Authorization;

namespace Com.Danliris.Service.Spinning.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/yarn-output-production")]
    [Authorize]
    public class WinderOutputProductionsCreateController : BasicController<SpinningDbContext, WinderOutputProductionService, WinderOutputProductionViewModel, WinderOutputProduction>
    {
        private static readonly string ApiVersion = "1.0";
        public WinderOutputProductionsCreateController(WinderOutputProductionService service) : base(service, ApiVersion)
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOutput([FromBody] WinderOutputProductionCreateViewModel ViewModel)
        {
            try
            {
                this.Validate(ViewModel);
                List<WinderOutputProduction> models = new List<WinderOutputProduction>();
                foreach (WinderOutputProductionCreateViewModel.YarnOutputItemVM item in ViewModel.YarnOutputItems)
                {
                    WinderOutputProduction model = new WinderOutputProduction();
                    model.Date = (DateTime)ViewModel.Date;
                    model.Shift = ViewModel.Shift;
                    model.YarnId = ViewModel.Yarn.Id != null ? (int)ViewModel.Yarn.Id : 0;
                    model.YarnCode = ViewModel.Yarn.Code;
                    model.YarnName = ViewModel.Yarn.Name;
                    model.SpinningId = ViewModel.Spinning._id;
                    model.SpinningCode = ViewModel.Spinning.code;
                    model.SpinningName = ViewModel.Spinning.name;
                    model.MachineId = ViewModel.Machine._id;
                    model.MachineCode = ViewModel.Machine.code;
                    model.MachineName = ViewModel.Machine.name;
                    model.LotYarnId = ViewModel.LotYarn.Id != null ? (int)ViewModel.LotYarn.Id : 0; ;
                    model.LotYarnCode = ViewModel.LotYarn.Code;
                    model.LotYarnName = ViewModel.LotYarn.Lot;
                    model.BadOutput = (double)item.BadOutput;
                    model.GoodOutput = (double)item.GoodOutput;
                    model.DrumTotal = (double)item.DrumTotal;
                    model.YarnWeightPerCone = (double)item.YarnWeightPerCone;

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

        [HttpGet("download")]
        public async Task<IActionResult> DownloadXls(string Unit, string Yarn, DateTime? DateFrom, DateTime? DateTo)
        {

            try
            {
                Unit = String.IsNullOrWhiteSpace(Unit) ? "All" : Unit;
                Yarn = String.IsNullOrWhiteSpace(Yarn) ? "All" : Yarn;
                DateTime dateFrom = DateFrom == null ? new DateTime(1900, 1, 1) : Convert.ToDateTime(DateFrom);
                DateTime dateTo = DateTo == null ? DateTime.Now : Convert.ToDateTime(DateTo);
                var data = await Service.getData(Unit, Yarn, dateFrom, dateTo);
                byte[] xlsInBytes;
                var xls = Service.GenerateExcel(data);

                string filename = DateFrom == null || DateTo == null ? $"Spinning Output Production Report {DateTime.Now:dd-MM-yyyy}.xlsx" : $"Spinning Output Production Report {dateFrom:dd-MM-yyyy} to {dateTo:dd-MM-yyyy}.xlsx";

                xlsInBytes = xls.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReportList(string Unit, string Yarn, DateTime? DateFrom, DateTime? DateTo)
        {
            try
            {
                Unit = String.IsNullOrWhiteSpace(Unit) ? "All" : Unit;
                Yarn = String.IsNullOrWhiteSpace(Yarn) ? "All" : Yarn;
                DateTime dateFrom = DateFrom == null ? new DateTime(1900, 1, 1) : Convert.ToDateTime(DateFrom);
                DateTime dateTo = DateTo == null ? DateTime.Now : Convert.ToDateTime(DateTo);
                var data = await Service.getData(Unit, Yarn, dateFrom, dateTo);
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok(data);
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

        private void Validate(WinderOutputProductionCreateViewModel viewModel)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(viewModel, this.Service.ServiceProvider, null);

            if (!Validator.TryValidateObject(viewModel, validationContext, validationResults, true))
                throw new ServiceValidationExeption(validationContext, validationResults);
        }
    }
}
