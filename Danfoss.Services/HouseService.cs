using System.Collections.Generic;
using System.Threading.Tasks;
using Danfoss.Contracts;
using Danfoss.DataLayer;
using Danfoss.Entities;
using Microsoft.EntityFrameworkCore;
using Danfoss.Entities.Enums;
using System.Linq;
using System.Text.RegularExpressions;
using Danfoss.Services.Common;
using Danfoss.Services.Common.Validation;
using System;

namespace Danfoss.Services
{
    public class HouseService : IHouseSrvice
    {
        protected readonly DanfossDbContext Context;
        protected readonly IModelValidator<House> _validator;
        public HouseService(DanfossDbContext context, IModelValidator<House> validator)
        {
            _validator = validator;
            Context = context;
        }
        public async Task<int> CreateNewHouse(House house)
        {
            var street = house.Street.FormatWhitespaces();
            var houseNumber = house.HouseNumber.FormatWhitespaces();
            house.Street = street;
            house.HouseNumber = houseNumber;

            await _validator.Validate(house, null);

            await Context.Set<House>().AddAsync(house);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdateHouse(House house)
        {
            var entity = await Context.Set<House>()
                .Include(i => i.Counter)
                .FirstOrDefaultAsync(item => item.Id == house.Id);
            if (entity != null)
            {
                Context.Entry(entity).CurrentValues.SetValues(house);
                Context.Entry(entity.Counter).CurrentValues.SetValues(house.Counter);
                return await Context.SaveChangesAsync();
            }
            throw new Exception($"Дом с Id={house.Id} не найден");
        }

        public async Task<int> DeleteHouse(int houseId)
        {
            var house = await GetHauseById(houseId);
            Context.Remove(house);
            return await Context.SaveChangesAsync();
        }

        public async Task<House> GetHauseById(int houseId) => await Context.Set<House>().Where(h => h.Id == houseId).FirstOrDefaultAsync();


        public async Task<IList<House>> GetHouseList() => await Context.House.Include(c => c.Counter).ToListAsync();


        public async Task<IList<House>> GetHousesByCounter(CounterMeter counterMeter)
        {
            return await GetMeterQuery(counterMeter).ToListAsync();
        }

        private IQueryable<House> GetMeterQuery(CounterMeter counterMeter)
        {
            return counterMeter == CounterMeter.MaxValue ? 
                Context.Set<House>().Where(t => (t.Counter.Value == Context.Set<Counter>().Max(c => c.Value))) : 
                Context.Set<House>().Where(t => (t.Counter.Value == Context.Set<Counter>().Min(c => c.Value)));
        }
    }
}
