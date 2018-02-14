using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class LotYarnViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Lot { get; set; }
        public YarnVM Yarn { get; set; }
        public UnitVM Unit { get; set; }
        public MachineVM Machine { get; set; }

        public class YarnVM
        {
            public int? Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class UnitVM
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Lot))
                yield return new ValidationResult("Lot Benang harus diisi", new List<string> { "Lot" });
            if (this.Yarn == null || this.Yarn.Id == 0)
                yield return new ValidationResult("Benang harus diisi", new List<string> { "YarnId" });
            if (this.Unit == null || string.IsNullOrWhiteSpace(this.Unit._id))
                yield return new ValidationResult("Benang harus diisi", new List<string> { "UnitId" });
            if (this.Machine == null || string.IsNullOrWhiteSpace(this.Machine._id))
                yield return new ValidationResult("Benang harus diisi", new List<string> { "MachineId" });
        }
    }
}
