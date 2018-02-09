using Com.Danliris.Service.Spinning.Lib.Services;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Spinning.Lib.Models
{
    public class LotYarn : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Lot { get; set; }
        public int YarnId { get; set; }
        public int UnitId { get; set; }
        public virtual Yarn Yarn { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            LotYarnService service = validationContext.GetService<LotYarnService>();

            if (service.DbSet.Count(r => r.Id != this.Id && r.Name.Equals(this.Name) && r._IsDeleted.Equals(false)) > 0)
                yield return new ValidationResult("Nama lot Benang sudah ada", new List<string> { "Name" });
        }
    }
}
