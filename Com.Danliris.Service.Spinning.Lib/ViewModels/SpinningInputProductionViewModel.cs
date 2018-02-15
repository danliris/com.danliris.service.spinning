using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class SpinningInputProductionViewModel : BasicViewModel, IValidatableObject
    {
        public string NomorInputProduksi { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public int YarnId { get; set; }
        public string YarnName { get; set; }
        public string Lot { get; set; }
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public double Counter { get; set; }
        public double Hank { get; set; }
        public virtual List<Input> input { get; set; }

        public class Input
        {
            public double Counter { get; set; }
            public double Hank { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Lot))
                yield return new ValidationResult("Lot harus diisi", new List<string> { "Lot" });
            if (this.Date == null || this.Date == DateTime.MinValue)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            if (this.MachineId == null || string.IsNullOrWhiteSpace(this.MachineId))
                yield return new ValidationResult("Mesin harus diisi", new List<string> { "MachineId" });
            if (this.YarnId == null || this.YarnId == 0)
                yield return new ValidationResult("Benang harus diisi", new List<string> { "YarnId" });
            if (this.UnitId == null || string.IsNullOrWhiteSpace(this.UnitId))
                yield return new ValidationResult("Unit harus diisi", new List<string> { "UnitId" });
            if (this.Counter == 0)
                yield return new ValidationResult("Counter Harus di isi", new List<string> { "Counter" });
        }
    }
}
