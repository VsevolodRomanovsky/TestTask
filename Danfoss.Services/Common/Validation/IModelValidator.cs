using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Danfoss.Services.Common.Validation
{
    public interface IModelValidator<in TModel>
    {
        Task<IEnumerable<ValidationResult>> Validate(TModel model, ValidationContext validationContext);
    }
}
