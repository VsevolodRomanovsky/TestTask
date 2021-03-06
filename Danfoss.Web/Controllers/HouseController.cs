﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Danfoss.Contracts;
using Danfoss.Entities;
using Danfoss.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Danfoss.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {

        private readonly IHouseSrvice hauseService;

        /// <summary>
        /// Контроллер для работы с информацией по домам
        /// </summary>
        public HouseController(IHouseSrvice _hauseService)
        {
            hauseService = _hauseService;
        }

        /// <summary>
        /// Получить список всех домов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation("GetHouseList")]
        public async Task<IList<House>> Get()
        {
            var houseList = await hauseService.GetHouseList();
            return houseList;
        }

        /// <summary>
        /// Найти дома с наименьшим или наибольшим показанием счетчика воды
        /// </summary>
        /// <param name="counterMeter"> </param>
        [HttpGet("meter/{counterMeter}")]
        [SwaggerOperation("GetHouseByCounterMetter")]
        public async Task<IList<House>> GetByCounterMetter(CounterMeter counterMeter)
        {
            return await hauseService.GetHousesByCounter(counterMeter);
        }


        /// <summary>
        /// Найти дом по Id
        /// </summary>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("GetHouseById")]
        public async Task<House> Get(int id) => await hauseService.GetHauseById(id);

        /// <summary>
        /// Добавить новый дом
        /// </summary>
        [HttpPost]
        [SwaggerOperation("AddNewHouse")]
        public async Task<IActionResult> Post([FromBody] House house)
        {
            await hauseService.CreateNewHouse(house);
            return Ok(house);
        }

        /// <summary>
        /// Обновить дом
        /// </summary>
        [HttpPut]
        [SwaggerOperation("UpdateHouse")]
        public async Task<IActionResult> Put([FromBody] House house)
        {
            await hauseService.UpdateHouse(house);
            return Ok(house);
        }

        /// <summary>
        /// Удалить дом
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteHouseById")]
        public async Task<int> Delete(int id)
        {
            return await hauseService.DeleteHouse(id);
        }
    }
}
