using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class SpinningInputProductionViewModel : BasicViewModel, IValidatableObject
    {
        public string NomorInputProduksi { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public DateTime tanggal { get; set; }
        public string Shift { get; set; }
        public int YarnId { get; set; }
        public string YarnName { get; set; }
        public string Lot { get; set; }
        public List<InputModel> Input { get; set; }
        public class InputModel
        {
            public int Id{ get; set; }
            public int SpinningInputProductionId { get; set; }
            public String Code { get; set; }
            public int Counter { get; set; } 
            public int Hash { get; set; }
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Lot))
                yield return new ValidationResult("Lot harus diisi", new List<string> { "Lot" });
        }
    }
}
