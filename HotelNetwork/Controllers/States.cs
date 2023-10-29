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
        // en un controlador los metodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una
        // api, se denomina ENDPOINT.
        // Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCION.

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]// Aqui concateno la URL inicial: URL = api/countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync();// aqui estoy yebdo a mi capa de Domain para traer la lista de paises
            if (countries == null || !countries.Any()) // el metodo Any () significa si hay al menos un elemento.
                                                       // el metodo !Any() significa si no hay absoluta/ nada.
            {
                return NotFound();// NotFound = 404 Http Status Code
            }
            return Ok(countries);// ok = 200 Http Status Code
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
                    return NotFound();// NotFound = 484 Http Status Code
                }
                return Ok(createdCountry);// Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(string.Format("El pais {0} ya existe.", country.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]// URL: api/countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountryByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var country = await _countryService.GetCountryByIdAsync(id);

            if (country == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(country);// ok = 200 Http Status Code
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]// URL: api/countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountryByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del pais es requerido!");

            var country = await _countryService.GetCountryByNameAsync(name);

            if (country == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(country);// ok = 200 Http Status Code
        }

        [HttpPut, ActionName("Edit")]// put es para editar
        [Route("Edit")]
        public async Task<ActionResult<Country>> EditCountryAsync(Country country)
        {
            try
            {
                var editedCountry = await _countryService.EditCountryAsync(country);
               
                return Ok(editedCountry);// Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(string.Format("{0} ya existe.", country.Name));
                
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Country>> DeleteCountryAsync(Guid id)
        {
           if (id == null) return BadRequest("Id es requerido!");

           var deletedCountry = await _countryService.DeleteCountryAsync(id);

           if (deletedCountry == null) return NotFound("Pais no encontrado");
           return Ok(deletedCountry);
        }
    }
}
