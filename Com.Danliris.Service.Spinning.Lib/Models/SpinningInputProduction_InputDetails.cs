using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Spinning.Lib.Models
{
    public class SpinningInputProduction_InputDetails : StandardEntity, IValidatableObject
    {
        public String Code { get; set; }
        public int Counter { get; set; }
        public int Hash { get; set; }
        public int SpinningInputProductionId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
