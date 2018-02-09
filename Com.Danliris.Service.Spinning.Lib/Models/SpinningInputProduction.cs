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
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public DateTime tanggal { get; set; }
        public string Shift { get; set; }
        public int YarnId { get; set; }
        public string YarnName { get; set; }
        public string Lot { get; set; }
        public ICollection<SpinningInputProduction_InputDetails> Input { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Lot))
                yield return new ValidationResult("Lot harus diisi", new List<string> { "Lot" });
        }
    }
}
