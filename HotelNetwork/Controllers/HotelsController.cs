using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController
    {
        private readonly IHotelService _countryService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        // en un controlador los metodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una
        // api, se denomina ENDPOINT.
        // Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCION.

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]// Aqui concateno la URL inicial: URL = api/hotels/get
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsAsync()
        {
            var hotels = await _hotelService.GetHotelsAsync();// aqui estoy yebdo a mi capa de Domain para traer la lista de paises
            if (hotels == null || !hotels.Any()) // el metodo Any () significa si hay al menos un elemento.
                                                       // el metodo !Any() significa si no hay absoluta/ nada.
            {
                return NotFound();// NotFound = 404 Http Status Code
            }
            return Ok(hotels);// ok = 200 Http Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateHotelAsync(Hotel hotel)
        {
            try
            {
                var createdHotel= await _hotelService.CreateHotelAsync(hotel);
                if (createdHotel== null)
                {
                    return NotFound();// NotFound = 484 Http Status Code
                }
                return Ok(createdHotel);// Retorne un 200 y el objeto 
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(string.Format("El pais {0} ya existe.", hotel.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]// URL: api/countries/get
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var hotel   = await _hotelService.GetHotelByIdAsync(id);

            if (hotel == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(hotel);// ok = 200 Http Status Code
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]// URL: api/countries/get
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del hotel es requerido!");

            var hotel = await _hotelService.GetHotelByNameAsync(name);

            if (hotel == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(hotel);// ok = 200 Http Status Code
        }

        [HttpPut, ActionName("Edit")]// put es para editar
        [Route("Edit")]
        public async Task<ActionResult<Hotel>> EditHotelAsync(Hotel hotel)
        {
            try
            {
                var editedHotel = await _hotelService.EditHotelAsync(hotel);

                return Ok(editedHotel);// Retorne un 200 y el objeto
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(string.Format("{0} ya existe.", hotel.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Hotel>> DeleteHotelAsync(Guid id)    
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedHotel= await _hotelService.DeleteHotelAsync(id);

            if (deletedHotel== null) return NotFound("Hotel no encontrado");
            return Ok(deletedHotel);
        }
    }
}
