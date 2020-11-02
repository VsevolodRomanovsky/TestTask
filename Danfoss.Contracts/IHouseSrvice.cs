using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Danfoss.Entities;
using Danfoss.Entities.Enums;

namespace Danfoss.Contracts
{
    public interface IHouseSrvice
    {
        public Task<int> CreateNewHouse(House House);
        public Task<int> DeleteHouse(int houseId);
        public Task<House> GetHauseById(int houseId);
        public Task<IList<House>> GetHouseList();
        public Task<IList<House>> GetHousesByCounter(CounterMeter counterMeter);
    }
}
