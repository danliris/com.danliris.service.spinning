using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Spinning.Lib.Models
{
    public class WinderOutputProduction : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public string SpinningId { get; set; }
        public string SpinningCode { get; set; }
        public string SpinningName { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string MachineId { get; set; }
        public string MachineCode { get; set; }
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
            return new List<ValidationResult>();
        }
    }
}
