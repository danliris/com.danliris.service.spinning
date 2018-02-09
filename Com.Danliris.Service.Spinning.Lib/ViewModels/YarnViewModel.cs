using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class YarnViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Ne { get; set; }
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Nama Benang harus diisi", new List<string> { "Name" });

            if  (this.Ne == 0)
                yield return new ValidationResult("Nomor Ne harus lebih besar dari 0", new List<string> { "Ne" });
        }
    }
}
