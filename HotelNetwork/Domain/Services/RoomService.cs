using HotelNetwork.DAL.Entities;
using HotelNetwork.DAL;
using Microsoft.EntityFrameworkCore;

namespace HotelNetwork.Domain.Services  
{
    public class RoomService
    {
        private readonly DataBaseContext _context;
        public RoomService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();// Aqui lo que hago es traerme todos los datos
                                                       //tengo en mi tabla Rooms

        }
        public async Task<Room> CreateRoomAsync(Room room)
        {
            try
            {
                room.Id = Guid.NewGuid();// asi se asigna automaticamente un ID a un nuevo registro
                room.CreateDate = DateTime.Now;

                _context.Rooms.Add(room);//Aqui estoy creado el objedo Room en el contexto de mi BD
                await _context.SaveChangesAsync();// Aqui ya estoy yendo a la BD para hacer el INSERT en la tabla Rooms 

                return room;
            }
            catch (DbUpdateException dbUpdateException)
            {
                // Esta exception no captura un mensaje cuando el pais YA EXISTE (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?

            }

        }

        public async Task<Room> GetRoomByIdAsync(Guid id)
        {
            //return await _context.Rooms.FindAsync(id); // FindAsyn es un metodo propio del DbContext (Dbset)
            //return await _context.Rooms.FirstAsync(x => x.Id == id);// FirstAsync es un metodo de EF CORE
            return await _context.Rooms.FirstOrDefaultAsync(c => c.Id == id); // FirstOrDefaultAsync es un metodo de EF CORE
        }

        public async Task<Room> GetRoomByNameAsync(string name)
        {
            return await _context.Rooms.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Room> EditRoomAsync(Room room)
        {
            try
            {
                room.ModifiedDate = DateTime.Now;

                _context.Rooms.Update(room);//El metodo Update que es de EF CORE me sirve para actualizar un objeto
                await _context.SaveChangesAsync();

                return room;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
        public async Task<Room> DeleteRoomAsync(Guid id)
        {
            try
            {
                // Aqui, con el ID que traigo desde el controller, estoy recuperando el pais que luego voy a eliminar
                // ese pais que recupero lo guardo en la variable room
                var room = await _context.Rooms.FirstOrDefaultAsync(c => c.Id == id);
                if (room == null) return null; // si el pais no existe, entonces me retorna un NULL

                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();

                return room;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
    }
}
