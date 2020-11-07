using Danfoss.Contracts;
using Danfoss.DataLayer;
using Danfoss.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Danfoss.Services
{
    public class CounterService : ICounterService
    {
        protected readonly DanfossDbContext Context;
        public CounterService(DanfossDbContext context)
        {
            Context = context;
        }
        public async Task<int> AddValueByHouseId(int houseId, int value)
        {
            var counter = await Context.Set<Counter>().Where(c => c.HouseId == houseId).FirstOrDefaultAsync();
            if(counter != null)
            {
                counter.Value = value;
                return await Context.SaveChangesAsync();
            }
            throw new NullReferenceException($"Дом c id '{houseId}' не найден");
        }

        public async Task<int> AddValueBySerialNumber(string serialNumber, int value)
        {
            var counter = await Context.Set<Counter>().Where(c => c.SerialNumber == serialNumber).FirstOrDefaultAsync();
            if (counter != null)
            {
                counter.Value = value;
                return await Context.SaveChangesAsync();
            }
            throw new NullReferenceException($"Не с серийным номером '{serialNumber}' найден счетчик ");
        }

        public async Task<int> RegisterCounter(Counter counter)
        {
            await Context.Set<Counter>().AddAsync(counter);
            return await Context.SaveChangesAsync();
        }
    }
}
