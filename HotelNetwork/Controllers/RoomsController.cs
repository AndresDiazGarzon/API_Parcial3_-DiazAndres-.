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

        private readonly IRoomService _roomService;
        private Room room;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        // en un controlador los metodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una
        // api, se denomina ENDPOINT.
        // Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCION.

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]// Aqui concateno la URL inicial: URL = api/rooms/get
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsAsync()
        {
            var rooms = await _roomService.GetRoomsAsync();// aqui estoy yebdo a mi capa de Domain para traer la lista de paises
            if (rooms == null || !rooms.Any()) // el metodo Any () significa si hay al menos un elemento.
                                                       // el metodo !Any() significa si no hay absoluta/ nada.
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
                var createdRoom = await _roomService.CreateRoomAsync(room);
                if (createdRoom == null)
                {
                    return NotFound();// NotFound = 484 Http Status Code
                }
                return Ok(editedRoom: createdRoom);// Retorne un 200 y el objeto Room
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(v: string.Format("El pais {0} ya existe.", room.Name));
                }
                return Conflict(ex.Message);
            }
        }

        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]// URL: api/rooms/get
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var room = await _roomService.GetRoomByIdAsync(id);

            if (room == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(room);// ok = 200 Http Status Code
        }

        public Room GetRoom()
        {
            return room;
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]// URL: api/rooms/get
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomByNameAsync(string name, Room room)
        {
            if (name == null) return BadRequest("Nombre del pais es requerido!");


            if ((Room?)await _roomService.GetRoomByNameAsync(name) == null) return NotFound();// NotFound = 404 Http Status Code

            return Ok(editedRoom: await _roomService.GetRoomByNameAsync(name));// ok = 200 Http Status Code
        }

        private ActionResult<IEnumerable<Room>> BadRequest(string v)
        {
            throw new NotImplementedException();
        }

        private ActionResult<IEnumerable<Room>> NotFound(string v)
        {
            throw new NotImplementedException();
        }

        [HttpPut, ActionName("Edit")]// put es para editar
        [Route("Edit")]
        public async Task<ActionResult<Room>> EditRoomAsync(Room room)
        {
            try
            {
                var editedRoom = await _roomService.EditRoomAsync(room);

                return Ok(editedRoom);// Retorne un 200 y el objeto Room
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(string.Format("{0} ya existe.", room.Name));

                return Conflict(ex.Message);
            }
        }

        private ActionResult<Room> Conflict(string v)
        {
            throw new NotImplementedException();
        }

        private ActionResult<Room> Ok(Room editedRoom)
        {
            throw new NotImplementedException();
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Room>> DeleteRoomAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedRoom = await _roomService.DeleteRoomAsync(id);

            if (deletedRoom == null) return NotFound("Pais no encontrado");
            return Ok(deletedRoom);
        }
    }
}
