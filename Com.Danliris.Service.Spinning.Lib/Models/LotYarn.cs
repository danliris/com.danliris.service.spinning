using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Spinning.Lib.Models
{
    public class LotYarn : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string Lot { get; set; }
        public int YarnId { get; set; }
        public string YarnCode { get; set; }
        public string YarnName { get; set; }
        public string UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string MachineId { get; set; }
        public string MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            LotYarnService service = validationContext.GetService<LotYarnService>();

            if (service.DbSet.Count(r => r.Id != this.Id && r.Lot.Equals(this.Lot) && r._IsDeleted.Equals(false)) > 0)
                yield return new ValidationResult("Nama lot Benang sudah ada", new List<string> { "Lot" });
        }
    }
}
