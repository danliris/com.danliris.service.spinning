using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class YarnOutputProductionCreateViewModel : BasicViewModel, IValidatableObject
    {
        public SpinningVm Spinning { get; set; }
        public MachineVM Machine { get; set; }
        public LotYarnVM LotYarn { get; set; }
        public YarnVM Yarn { get; set; }
        public List<YarnOutputItemVM> YarnOutputItems { get; set; }
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
        public class YarnOutputItemVM
        {
            public double? YarnWeightPerCone { get; set; }
            public double? GoodOutput { get; set; }
            public double? BadOutput { get; set; }
            public double? DrumTotal { get; set; }
        }
        public DateTime? Date { get; set; }
        public string Shift { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Spinning == null || string.IsNullOrWhiteSpace(this.Spinning._id))
                yield return new ValidationResult("Nama Spinning harus diisi", new List<string> { "SpinningId" });
            if (this.Shift == null || string.IsNullOrWhiteSpace(this.Shift))
                yield return new ValidationResult("Shift harus diisi", new List<string> { "Shift" });
            if (this.Date == null || this.Date == DateTime.MinValue)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            else if (this.Date > DateTime.Today)
                yield return new ValidationResult("Tanggal tidak boleh lebih besar dari sekarang", new List<string> { "Date" });
            if (this.Machine == null || string.IsNullOrWhiteSpace(this.Machine._id))
                yield return new ValidationResult("Mesin harus diisi", new List<string> { "MachineId" });
            if (this.Yarn == null || this.Yarn.Id == 0)
                yield return new ValidationResult("Benang harus diisi", new List<string> { "YarnId" });
            if (this.LotYarn == null || this.LotYarn.Id == 0)
                yield return new ValidationResult("Lot Benang harus diisi", new List<string> { "LotYarnId" });

            int Count = 0;
            string yarnOutputItemsError = "[";

            if (this.YarnOutputItems == null || this.YarnOutputItems.Count.Equals(0))
            {
                yield return new ValidationResult("Detail Benang harus diisi", new List<string> { "YarnOutputItems" });
            }
            else
            {
                foreach (YarnOutputItemVM yarnOutputItem in this.YarnOutputItems)
                {
                    yarnOutputItemsError += "{";
                    if (yarnOutputItem.YarnWeightPerCone <= 0)
                    {
                        Count++;
                        yarnOutputItemsError += "YarnWeightPerCone: 'Berat per Cone harus lebih besar dari 0', ";
                    }
                    else if (yarnOutputItem.YarnWeightPerCone == null)
                    {
                        Count++;
                        yarnOutputItemsError += "YarnWeightPerCone: 'Berat per Cone harus lebih besar dari 0', ";
                    }
                    if (yarnOutputItem.GoodOutput + yarnOutputItem.BadOutput <= 0 || (yarnOutputItem.BadOutput == null && yarnOutputItem.GoodOutput == null))
                    {
                        Count++;
                        yarnOutputItemsError += "GoodOutput: 'Good Output harus lebih besar dari 0', ";
                        yarnOutputItemsError += "BadOutput: 'Bad Output harus lebih besar dari 0', ";
                    }
                    else if (yarnOutputItem.GoodOutput == 0)
                    {
                        Count++;
                        yarnOutputItemsError += "GoodOutput: 'Good Output harus lebih besar dari 0', ";
                    }
                    if (yarnOutputItem.DrumTotal <= 0)
                    {
                        Count++;
                        yarnOutputItemsError += "DrumTotal: 'Total Drum harus lebih besar dari 0', ";
                    }
                    else if (yarnOutputItem.DrumTotal == null)
                    {
                        Count++;
                        yarnOutputItemsError += "DrumTotal: 'Total Drum harus lebih besar dari 0', ";
                    }

                    yarnOutputItemsError += "},";
                }
            }

            yarnOutputItemsError += "]";

            if (Count > 0)
            {
                yield return new ValidationResult(yarnOutputItemsError, new List<string> { "YarnOutputItems" });
            }
        }
    }
}
