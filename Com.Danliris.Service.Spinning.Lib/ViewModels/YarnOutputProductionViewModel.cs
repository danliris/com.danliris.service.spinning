using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class YarnOutputProductionViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string SpinningId { get; set; }
        public string Spinning { get; set; }
        public DateTime? Date { get; set; }
        public string Shift { get; set; }
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public int? YarnId { get; set; }
        public string YarnCode { get; set; }
        public string YarnName { get; set; }
        public int? LotYarnId { get; set; }
        public string LotYarnName { get; set; }
        public string LotYarnCode { get; set; }
        public double? YarnWeightPerCone { get; set; }
        public double? GoodOutput { get; set; }
        public double? BadOutput { get; set; }
        public double? DrumTotal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.SpinningId))
                yield return new ValidationResult("Nama Spinning harus diisi", new List<string> { "SpinningId" });
            if (this.Date == null || this.Date == DateTime.MinValue)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            else if (this.Date > DateTime.Today)
                yield return new ValidationResult("Tanggal tidak boleh lebih besar dari sekarang", new List<string> { "Date" });
            if (string.IsNullOrWhiteSpace(this.Shift))
                yield return new ValidationResult("Shift tidak boleh kosong", new List<string> { "Shift" });
            if (string.IsNullOrWhiteSpace(this.MachineId))
                yield return new ValidationResult("Mesin harus diisi", new List<string> { "MachineId" });
            if (this.YarnId <= 0)
                yield return new ValidationResult("Benang harus diisi", new List<string> { "YarnId" });
            else if (this.YarnId == null)
                yield return new ValidationResult("Benang harus diisi", new List<string> { "YarnId" });
            if (this.LotYarnId <= 0)
                yield return new ValidationResult("Lot Benang harus diisi", new List<string> { "LotYarnId" });
            else if (this.LotYarnId == null)
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
