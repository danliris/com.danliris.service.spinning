using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Spinning.Lib.Models
{
    public class YarnOutputProduction : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public string SpinningId { get; set; }
        public string Spinning { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public int YarnId { get; set; }
        public string YarnCode { get; set; }
        public string YarnName { get; set; }
        public int LotYarnId { get; set; }
        public string LotYarnName { get; set; }
        public string LotYarnCode { get; set; }
        public double YarnWeightPerCone { get; set; }
        public double GoodOutput { get; set; }
        public double BadOutput { get; set; }
        public double DrumTotal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            YarnOutputProductionService service = validationContext.GetService<YarnOutputProductionService>();
            if (service.DbSet.Count(r => r.Id != this.Id && r.Code.Equals(this.Code) && r._IsDeleted.Equals(false)) > 0)
                yield return new ValidationResult("Code duplicat", new List<string> { "Code" });
        }
    }
}
