using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : HotelsControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<void> GetHotelsAsync()
        {
            return await _hotelService.GetHotelsAsync();
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateHotelAsync(Hotel hotel)
        {
            try
            {
                var createdHotel = await _hotelService.CreateHotelAsync(hotel);
                if (createdHotel == null)
                {
                    return NotFound();// NotFound = 484 Http Status Code
                }
                return Ok(createdHotel);// Retorne un 200 y el objeto Hotel
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

        private ActionResult Conflict(string v)
        {
            throw new NotImplementedException();
        }

        private ActionResult Ok(Hotel createdHotel)
        {
            throw new NotImplementedException();
        }

        private ActionResult NotFound(string v)
        {
            throw new NotImplementedException();
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]// URL: api/hotels/get
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelByIdAsync(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id es requerido!");
            }

            var hotel = await _hotelService.GetHotelByIdAsync(id);

            if (hotel == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(hotel);// ok = 200 Http Status Code
        }

        private ActionResult<IEnumerable<Hotel>> NotFound()
        {
            throw new NotImplementedException();
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]// URL: api/hotels/get
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del pais es requerido!");

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

                return Ok(editedHotel);// Retorne un 200 y el objeto Hotel
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

            var deletedHotel = await _hotelService.DeleteHotelAsync(id);

            if (deletedHotel == null) return NotFound("Hotel no encontrado");
            return Ok(deletedHotel);
        }

        private ActionResult<Hotel> BadRequest(string v)
        {
            throw new NotImplementedException();
        }
    }
}
