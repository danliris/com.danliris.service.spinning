using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Spinning.Lib.Models
{
    public class SpinningInputProduction : StandardEntity, IValidatableObject
    {
        public string NomorInputProduksi { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public int YarnId { get; set; }
        public string YarnName { get; set; }
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public string Lot { get; set; }
        public double Counter { get; set; }
        public double Hank { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
