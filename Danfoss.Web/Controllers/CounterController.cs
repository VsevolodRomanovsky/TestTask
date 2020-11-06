using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Danfoss.Contracts;
using Danfoss.Entities;
using Danfoss.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Danfoss.Web.Controllers
{
    [Route("api/[controller]")]
    public class CounterController : ControllerBase
    {
        private readonly ICounterService counterService;

        /// <summary>
        /// Контроллер для работы с информацией по счетчикам воды
        /// </summary>
        public CounterController(ICounterService _counterController)
        {
            counterService = _counterController;
        }


        /// <summary>
        /// Добавить новый счетчик
        /// </summary>
        [HttpPost]
        public async Task<int> Post([FromBody]Counter counter)
        {
                return await counterService.RegisterCounter(counter);
        }

        /// <summary>
        /// Добавить показания счетчика
        /// </summary>
        /// <param name="houseId">идентификатор дома</param>
        /// <param name="value">значение счетчика</param>
        [HttpGet("byhouse")]
        [ValidateActionParameters]
        public async Task<int> AddValueByHouseId([FromQuery, Required]int houseId, [FromQuery, Required, PositiveNumber]int value)
        {
            return await counterService.AddValueByHouseId(houseId, value);
        }

        /// <summary>
        /// Добавить показания счетчика
        /// </summary>
        /// <param name="serialNumber">серийный номер счетчика</param>
        /// <param name="value">значение счетчика</param>
        [HttpGet("byserialnumber")] 
        public async Task<int> AddValueBySerialNumber([FromQuery][Required]string serialNumber, [FromQuery][Required]int value)
        {
            return await counterService.AddValueBySerialNumber(serialNumber, value);
        }
    }
}
