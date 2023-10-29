using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using HotelNetwork.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
        public class HotelsController : Controller
        {
            private readonly IHotelService _hotelService;
            public HotelsController(IHotelService hotelService)      
            {
                _hotelService = hotelService;
            }

            //En un controlador los métodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una API, se denomina ENDPOINT.
            //Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCIÓN.

            [HttpGet, ActionName("Get")]
            [Route("Get")] //Aquí concateno la URL inicial: URL = api/hotels/get
            public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsAsync()
            {
                var hotels = await _hotelService.GetHotelsAsync(); //Aquí estoy yendo a mi capa de Domain para traerme la lista de países

            //El método Any() significa si hay al menos un elemento.
            
            //El Método !Any() significa si no hay absoluta/ nada.
            if (hotels == null || !hotels.Any())
                {
                    return NotFound(); //NotFound = 404 Http Status Code
                }

                return Ok(hotels); //Ok = 200 Http Status Code
            }

            [HttpPost, ActionName("Create")]
            [Route("Create")]
            public async Task<ActionResult> CreateHotelAsync(Hotel hotel)
            //
            {
                try
                {
                    var createdHotel = await _hotelService.CreateHotelAsync(hotel, hotel.CreateDate);

                    if (createdHotel == null)
                    {
                        return NotFound(); //NotFound = 404 Http Status Code
                    }

                    return Ok(createdHotel); //Retorne un 200 y el objeto Hotel
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("duplicate"))
                    {
                        return Conflict(String.Format("El país {0} ya existe.", hotel.Name)); //Confilct = 409 Http Status Code Error
                    }

                    return Conflict(ex.Message);
                }
            }

            [HttpGet, ActionName("Get")]
            [Route("GetById/{id}")] //URL: api/hotels/get
            public async Task<ActionResult<Hotel>> GetHotelByIdAsync(Guid id)
            {
                if (id == null) return BadRequest("Id es requerido!");

                var hotel = await _hotelService.GetHotelByIdAsync(id);

                if (hotel == null) return NotFound(); // 404

                return Ok(hotel); // 200
            }

            [HttpGet, ActionName("Get")]
            [Route("GetByName/{name}")] //URL: api/hotels/get
            public async Task<ActionResult<Hotel>> GetHotelByNameAsync(string name)
            {
                if (name == null) return BadRequest("Nombre del país requerido!");

                var hotel = await _hotelService.GetHotelByNameAsync(name);

                if (hotel == null) return NotFound(); // 404

                return Ok(hotel); // 200
            }

            [HttpPut, ActionName("Edit")]
            [Route("Edit")]
            public async Task<ActionResult<Hotel>> EditHotelAsync(Hotel hotel)
            {
                try
                {
                    var editedHotel = await _hotelService.EditHotelAsync(hotel);
                    return Ok(editedHotel);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("duplicate"))
                        return Conflict(String.Format("{0} ya existe", hotel.Name));

                    return Conflict(ex.Message);
                }
            }

            [HttpDelete, ActionName("Delete")]
            [Route("Delete")]
            public async Task<ActionResult<Hotel>> DeleteHotelAsync(Guid id)
            {
                if (id == null) return BadRequest("Id es requerido!");

                var deletedHotel = await _hotelService.DeleteHotelAsync(id);

                if (deletedHotel == null) return NotFound("País no encontrado!");

                return Ok(deletedHotel);
            }


        }
    }
