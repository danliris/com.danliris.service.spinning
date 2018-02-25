using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class WinderOutputProductionViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public SpinningVm Spinning { get; set; }
        public MachineVM Machine { get; set; }
        public LotYarnVM LotYarn { get; set; }
        public YarnVM Yarn { get; set; }
        public class SpinningVm
        {
            public string _id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
        }
        public class MachineVM
        {
            public string _id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
        }
        public class LotYarnVM
        {
            public int? Id { get; set; }
            public string Code { get; set; }
            public string Lot { get; set; }
        }
        public class YarnVM
        {
            public int? Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }
        public DateTime? Date { get; set; }
        public string Shift { get; set; }
        public double? YarnWeightPerCone { get; set; }
        public double? GoodOutput { get; set; }
        public double? BadOutput { get; set; }
        public double? DrumTotal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Spinning == null || string.IsNullOrWhiteSpace(this.Spinning._id))
                yield return new ValidationResult("Nama Spinning harus diisi", new List<string> { "SpinningId" });
            if (this.Date == null || this.Date == DateTime.MinValue)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            else if (this.Date > DateTime.Today)
                yield return new ValidationResult("Tanggal tidak boleh lebih besar dari sekarang", new List<string> { "Date" });
            if (string.IsNullOrWhiteSpace(this.Shift))
                yield return new ValidationResult("Shift tidak boleh kosong", new List<string> { "Shift" });
            if (this.Machine == null || string.IsNullOrWhiteSpace(this.Machine._id))
                yield return new ValidationResult("Mesin harus diisi", new List<string> { "MachineId" });
            if (this.Yarn == null || this.Yarn.Id == 0)
                yield return new ValidationResult("Benang harus diisi", new List<string> { "YarnId" });
            if (this.LotYarn == null || this.LotYarn.Id == 0)
                yield return new ValidationResult("Lot Benang harus diisi", new List<string> { "LotYarnId" });
            if (this.YarnWeightPerCone <= 0)
                yield return new ValidationResult("Berat per Cone harus lebih besar dari 0", new List<string> { "YarnWeightPerCone" });
            else if (this.YarnWeightPerCone == null)
                yield return new ValidationResult("Berat per Cone harus lebih besar dari 0", new List<string> { "YarnWeightPerCone" });
            if (this.GoodOutput + this.BadOutput <= 0 || (this.BadOutput == null && this.GoodOutput == null))
            {
                yield return new ValidationResult("Good Output harus lebih besar dari 0", new List<string> { "GoodOutput" });
                yield return new ValidationResult("Bad Output harus lebih besar dari 0", new List<string> { "BadOutput" });
            }
            if (this.DrumTotal <= 0)
                yield return new ValidationResult("Jumlah Drum harus lebih besar dari 0", new List<string> { "DrumTotal" });
            else if (this.DrumTotal == null)
                yield return new ValidationResult("Jumlah Drum harus lebih besar dari 0", new List<string> { "DrumTotal" });
        }
    }
}
