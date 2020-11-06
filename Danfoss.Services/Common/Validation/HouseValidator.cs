using Danfoss.DataLayer;
using Danfoss.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Danfoss.Services.Common.Validation
{
    /// <summary> Валидация модели <see cref="House"/> </summary>
    public class HouseNewValidator : IModelValidator<House>
    {
        protected readonly DanfossDbContext Context;
        public HouseNewValidator(DanfossDbContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<ValidationResult>> Validate(House model, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (model == null)
            {
                results.Add(new ValidationResult($"Значение модели не корректно", new[] { nameof(model.Id) }));
                return results;
            }

            if (string.IsNullOrEmpty(model.Street) || string.IsNullOrEmpty(model.HouseNumber))
            {
                results.Add(new ValidationResult($"Нельзя создать дом с не полным адресом"));
                return results;
            }

            var house = await Context.House.FirstOrDefaultAsync(h => h.Street == model.Street && h.HouseNumber == model.HouseNumber);

            if(house != null)
            {
                results.Add(new ValidationResult($"Дом с адресом {model.Street} {model.HouseNumber} существует"));
                return results;
            }

            return results;
        }
    }
}
