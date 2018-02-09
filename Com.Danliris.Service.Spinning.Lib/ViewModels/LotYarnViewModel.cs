using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class LotYarnViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Lot { get; set; }
        public YarnVM Yarn { get; set; }
        public UnitVM Unit { get; set; }

        public class YarnVM
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class UnitVM
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Nama Benang harus diisi", new List<string> { "Name" });
            if (this.Yarn == null || this.Yarn.Id == 0)
                yield return new ValidationResult("Benang harus diisi", new List<string> { "Benang" });

        }
    }
}
