using Microsoft.AspNetCore.Mvc;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using HotelNetwork.Domain.Services;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : Controller

    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("GetAll")] // Usando la ruta "GetAll"
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsAsync()
        {
            var rooms = await _roomService.GetRoomsAsync();
            if (rooms == null || !rooms.Any())
            {
                return NotFound();
            }
            return Ok(rooms);
        }

        [HttpPost("Create")] // Usando la ruta "Create"
        public async Task<ActionResult> CreateRoomAsync(Room room)
        {
            try
            {
                var createdRoom = await _roomService.CreateRoomAsync(room);
                if (createdRoom == null)
                {
                    return NotFound();
                }
                return Ok(createdRoom);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(string.Format("La habitación {0} ya existe.", room.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Room>> GetRoomByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id es requerido!");
            }

            var room = await _roomService.GetRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<Room>> GetRoomByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Nombre de la habitación es requerido!");
            }

            var room = await _roomService.GetRoomByNameAsync(name);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<Room>> EditRoomAsync(Room room)
        {
            try
            {
                var editedRoom = await _roomService.EditRoomAsync(room);

                return Ok(editedRoom);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(string.Format("La habitación {0} ya existe.", room.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Room>> DeleteRoomAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id es requerido!");
            }

            var deletedRoom = await _roomService.DeleteRoomAsync(id);

            if (deletedRoom == null)
            {
                return NotFound("Habitación no encontrada");
            }
            return Ok(deletedRoom);
        }
    }
}
