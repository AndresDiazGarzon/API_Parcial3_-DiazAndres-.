using Microsoft.AspNetCore.Mvc;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController
    {
        private readonly IRoomService _hotelService;

        public RoomsController(IRoomService roomService)    
        {
            _roomService = roomService;
        }
        // en un controlador los metodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una
        // api, se denomina ENDPOINT.
        // Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCION.

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]// Aqui concateno la URL inicial: URL = api/hotels/get
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsAsync()
        {
            var rooms = await _roomService.GetRoomsAsync();
            if (rooms == null || !rooms.Any()) 
            {
                return NotFound();// NotFound = 404 Http Status Code
            }
            return Ok(rooms);// ok = 200 Http Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateRoomAsync(Room room)
        {
            try
            {
                var createdRoom= await _roomService.CreateRoomAsync(room);
                if (createdRoom== null)
                {
                    return NotFound();// NotFound = 484 Http Status Code
                }
                return Ok(createdRoom);// Retorne un 200 y el objeto 
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(string.Format("El pais {0} ya existe.", room.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]// URL: api/countries/get
        public async Task<ActionResult<IEnumerable<room>>> GetRoomByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var room   = await _hotelService.GetRoomByIdAsync(id);

            if (room == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(room);// ok = 200 Http Status Code
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]// URL: api/countries/get
        public async Task<ActionResult<IEnumerable<room>>> GetRoomByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del room es requerido!");

            var room = await _hotelService.GetRoomByNameAsync(name);

            if (room == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(room);// ok = 200 Http Status Code
        }

        [HttpPut, ActionName("Edit")]// put es para editar
        [Route("Edit")]
        public async Task<ActionResult<room>> EditRoomAsync(room room)
        {
            try
            {
                var editedRoom = await _hotelService.EditRoomAsync(room);

                return Ok(editedRoom);// Retorne un 200 y el objeto
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(string.Format("{0} ya existe.", room.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<room>> DeleteRoomAsync(Guid id)    
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedRoom= await _hotelService.DeleteRoomAsync(id);

            if (deletedRoom== null) return NotFound("room no encontrado");
            return Ok(deletedRoom);
        }
    }
}
