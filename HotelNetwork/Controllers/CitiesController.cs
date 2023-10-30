using DocumentFormat.OpenXml.Bibliography;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<City>>> GetCitysByStateIdAsync(Guid stateId)
        {
            var citys = await _cityService.GetCitysByStateIdAsync(stateId);
            if (citys == null || !citys.Any()) return NotFound();

            return Ok(citys);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCityAsync(City city, Guid stateId)
        {
            try
            {
                var createdCity = await _cityService.CreateCityAsync(city, stateId);

                if (createdCity == null) return NotFound();

                return Ok(createdCity);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El estado/departamento {0} ya existe.", city.Name));
                }

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]
        public async Task<ActionResult<City>> GetCityByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var city = await _cityService.GetCityByIdAsync(id);

            if (city == null) return NotFound();

            return Ok(city);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<City>> EditCityAsync(City city, Guid id)
        {
            try
            {
                var editedCity = await _cityService.EditCityAsync(city, id);
                return Ok(editedCity);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", city.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<City>> DeleteCityAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedCity = await _cityService.DeleteCityAsync(id);

            if (deletedCity == null) return NotFound("País no encontrado!");

            return Ok("City Deleted"); //in Ok() method you can send a message in swagger instead send the object
        }
    }
}
