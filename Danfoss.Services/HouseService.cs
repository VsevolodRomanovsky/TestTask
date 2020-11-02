using System.Collections.Generic;
using System.Threading.Tasks;
using Danfoss.Contracts;
using Danfoss.DataLayer;
using Danfoss.Entities;
using Microsoft.EntityFrameworkCore;
using Danfoss.Entities.Enums;
using System.Linq;

namespace Danfoss.Services
{
    public class HouseService : IHouseSrvice
    {
        protected readonly DanfossDbContext Context;
        public HouseService(DanfossDbContext context)
        {
            Context = context;
        }
        public async Task<int> CreateNewHouse(House house)
        {
            await Context.Set<House>().AddAsync(house);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteHouse(int houseId)
        {
            var house = await GetHauseById(houseId);
            Context.Remove(house);
            return await Context.SaveChangesAsync();
        }

        public async Task<House> GetHauseById(int houseId) => await Context.Set<House>().Where(h => h.Id == houseId).FirstOrDefaultAsync();


        public async Task<IList<House>> GetHouseList() => await Context.House.ToListAsync();


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
