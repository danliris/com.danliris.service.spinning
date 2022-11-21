using Com.Danliris.Service.Spinning.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Spinning.Lib.ViewModels
{
    public class MasterCountViewModel : BasicViewModel, IValidatableObject
    {
        public string Count { get; set; }
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Count == "")
            {
                yield return new ValidationResult("Count harus diisi", new List<string> { "Count" });
            }
        }


    }
}
