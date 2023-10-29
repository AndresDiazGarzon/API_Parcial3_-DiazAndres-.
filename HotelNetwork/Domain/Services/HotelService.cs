using HotelNetwork.DAL.Entities;
using HotelNetwork.DAL;
using Microsoft.EntityFrameworkCore;

namespace HotelNetwork.Domain.Services
{
    public class HotelService
    {
        private readonly DataBaseContext _context;
        public HotelService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels.ToListAsync();// Aqui lo que hago es traerme todos los datos
                                                          //tengo en mi tabla Hotels

        }
        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            try
            {
                hotel.Id = Guid.NewGuid();// asi se asigna automaticamente un ID a un nuevo registro
                hotel.CreateDate = DateTime.Now;

                _context.Hotels.Add(hotel);//Aqui estoy creado el objedo Hotel en el contexto de mi BD
                await _context.SaveChangesAsync();// Aqui ya estoy yendo a la BD para hacer el INSERT en la tabla Hotels 

                return hotel;
            }
            catch (DbUpdateException dbUpdateException)
            {
                // Esta exception no captura un mensaje cuando el pais YA EXISTE (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?

            }

        }

        public async Task<Hotel> GetHotelByIdAsync(Guid id)
        {
            //return await _context.Hotels.FindAsync(id); // FindAsyn es un metodo propio del DbContext (Dbset)
            //return await _context.Hotels.FirstAsync(x => x.Id == id);// FirstAsync es un metodo de EF CORE
            return await _context.Hotels.FirstOrDefaultAsync(c => c.Id == id); // FirstOrDefaultAsync es un metodo de EF CORE
        }

        public async Task<Hotel> GetHotelByNameAsync(string name)
        {
            return await _context.Hotels.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Hotel> EditHotelAsync(Hotel hotel)
        {
            try
            {
                hotel.ModifiedDate = DateTime.Now;

                _context.Hotels.Update(hotel);//El metodo Update que es de EF CORE me sirve para actualizar un objeto
                await _context.SaveChangesAsync();

                return hotel;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
        public async Task<Hotel> DeleteHotelAsync(Guid id)
        {
            try
            {
                // Aqui, con el ID que traigo desde el controller, estoy recuperando el pais que luego voy a eliminar
                // ese pais que recupero lo guardo en la variable hotel
                var hotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == id);
                if (hotel == null) return null; // si el pais no existe, entonces me retorna un NULL

                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();

                return hotel;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
    }
}
