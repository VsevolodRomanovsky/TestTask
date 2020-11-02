using Danfoss.Entities;
using System.Threading.Tasks;

namespace Danfoss.Contracts
{
    public interface ICounterService
    {
        public Task<int> AddValueBySerialNumber(string serialNumber, int value);
        public Task<int> AddValueByHouseId(int houseId, int value);
        public Task<int> RegisterCounter(Counter counter);
    }
}
