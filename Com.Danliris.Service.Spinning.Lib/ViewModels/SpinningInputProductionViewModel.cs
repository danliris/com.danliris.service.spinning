using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class SpinningInputProductionViewModel : BasicViewModel, IValidatableObject
    {
        public string NomorInputProduksi { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public yarn Yarn { get; set; }
        public string Lot { get; set; }
        public machine Machine { get; set; }
        public double Counter { get; set; }
        public double Hank { get; set; }
        public virtual List<Input> input { get; set; }
        public unit Unit { get; set; }

        public class unit
        {
            public string _id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
        }

        public class machine
        {
            public string _id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
        }

        public class yarn
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }
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
            if (this.Machine == null || string.IsNullOrWhiteSpace(this.Machine._id))
                yield return new ValidationResult("Mesin harus diisi", new List<string> { "MachineId" });
            if (this.Counter == 0)
                yield return new ValidationResult("Counter harus diisi", new List<string> { "Counter" });
            if (this.Unit == null || string.IsNullOrWhiteSpace(this.Unit._id))
                yield return new ValidationResult("Unit harus diisi", new List<string> { "UnitId" });
            if (this.Yarn == null ||(this.Yarn.Id == 0))
                yield return new ValidationResult("Unit harus diisi", new List<string> { "UnitId" });

        }
    }
}
