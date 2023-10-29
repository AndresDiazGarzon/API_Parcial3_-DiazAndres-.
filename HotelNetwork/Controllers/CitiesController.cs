﻿using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICountryService _countryService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }
        

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]// Aqui concateno la URL inicial: URL = api/countries/get
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesAsync()
        {
            var cities = await _cityService.GetCitiesAsync();// aqui estoy yebdo a mi capa de Domain para traer la lista de paises
            if (cities == null || !cities.Any()) // el metodo Any () significa si hay al menos un elemento.
                                                       // el metodo !Any() significa si no hay absoluta/ nada.
            {
                return NotFound();// NotFound = 404 Http Status Code
            }
            return Ok(cities);// ok = 200 Http Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(City city)
        {
            try
            {
                var createdCity = await _countryService.CreateCityAsync(city);
                if (createdCity == null)
                {
                    return NotFound();// NotFound = 484 Http Status Code
                }
                return Ok(createdCity);// Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(string.Format("El pais {0} ya existe.", city.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]// URL: api/cities/get
        public async Task<ActionResult<IEnumerable<City>>> GetCityByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var city = await _countryService.GetCityByIdAsync(id);

            if (city == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(city);// ok = 200 Http Status Code
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]// URL: api/countries/get
        public async Task<ActionResult<IEnumerable<City>>> GetCityyByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del pais es requerido!");

            var city = await _cityService.GetCityByNameAsync(name);

            if (city == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(city);// ok = 200 Http Status Code
        }

        [HttpPut, ActionName("Edit")]// put es para editar
        [Route("Edit")]
        public async Task<ActionResult<City>> EditCityAsync(City city)
        {
            try
            {
                var editedCity = await _cityService.EditCityAsync(city);

                return Ok(editedCity);// Retorne un 200 y el objeto 
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(string.Format("{0} ya existe.", city.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<City>> DeleteCityAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedCity = await _cityService.DeleteCityAsync(id);

            if (deletedCity == null) return NotFound("Pais no encontrado");
            return Ok(deletedCity);
        }
    }
}
