using Microsoft.AspNetCore.Mvc;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]// esta es la primera parte de la URL de esta API: URL = api/countries
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        //En un controlador los métodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una API, se denomina ENDPOINT.
        //Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCIÓN.

        [HttpGet, ActionName("Get")]
        [Route("Get")] //Aquí concateno la URL inicial: URL = api/countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync(); //Aquí estoy yendo a mi capa de Domain para traerme la lista de países

            //El método Any() significa si hay al menos un elemento.
            //El Método !Any() significa si no hay absoluta/ nada.
            if (countries == null || !countries.Any())
            {
                return NotFound(); //NotFound = 404 Http Status Code
            }

            return Ok(countries); //Ok = 200 Http Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(Country country)
        {
            try
            {
                var createdCountry = await _countryService.CreateCountryAsync(country);

                if (createdCountry == null)
                {
                    return NotFound(); //NotFound = 404 Http Status Code
                }

                return Ok(createdCountry); //Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El país {0} ya existe.", country.Name)); //Confilct = 409 Http Status Code Error
                }

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<ActionResult<Country>> GetCountryByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var country = await _countryService.GetCountryByIdAsync(id);

            if (country == null) return NotFound(); // 404

            return Ok(country); // 200
        }

        [HttpGet, ActionName("Get")]
        [Route("GetByName/{name}")] //URL: api/countries/get
        public async Task<ActionResult<Country>> GetCountryByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del país requerido!");

            var country = await _countryService.GetCountryByNameAsync(name);

            if (country == null) return NotFound(); // 404

            return Ok(country); // 200
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Country>> EditCountryAsync(Country country)
        {
            try
            {
                var editedCountry = await _countryService.EditCountryAsync(country);
                return Ok(editedCountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Country>> DeleteCountryAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedCountry = await _countryService.DeleteCountryAsync(id);

            if (deletedCountry == null) return NotFound("País no encontrado!");

            return Ok(deletedCountry);
        }
    }
}
