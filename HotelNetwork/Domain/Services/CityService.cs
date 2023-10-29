using HotelNetwork.DAL.Entities;
using HotelNetwork.DAL;
using Microsoft.EntityFrameworkCore;

namespace HotelNetwork.Domain.Services
{
    public class CityService
    {
        private readonly DataBaseContext _context;
        public CityService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.ToListAsync();// Aqui lo que hago es traerme todos los datos
                                                          //tengo en mi tabla Cities

        }
        public async Task<City> CreateCityAsync(City city)
        {
            try
            {
                city.Id = Guid.NewGuid();// asi se asigna automaticamente un ID a un nuevo registro
                city.CreateDate = DateTime.Now;

                _context.Cities.Add(city);//Aqui estoy creado el objedo City en el contexto de mi BD
                await _context.SaveChangesAsync();// Aqui ya estoy yendo a la BD para hacer el INSERT en la tabla Cities 

                return city;
            }
            catch (DbUpdateException dbUpdateException)
            {
                // Esta exception no captura un mensaje cuando el pais YA EXISTE (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?

            }

        }

        public async Task<City> GetCityByIdAsync(Guid id)
        {
            //return await _context.Cities.FindAsync(id); // FindAsyn es un metodo propio del DbContext (Dbset)
            //return await _context.Cities.FirstAsync(x => x.Id == id);// FirstAsync es un metodo de EF CORE
            return await _context.Cities.FirstOrDefaultAsync(c => c.Id == id); // FirstOrDefaultAsync es un metodo de EF CORE
        }

        public async Task<City> GetCityByNameAsync(string name)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<City> EditCityAsync(City city)
        {
            try
            {
                city.ModifiedDate = DateTime.Now;

                _context.Cities.Update(city);//El metodo Update que es de EF CORE me sirve para actualizar un objeto
                await _context.SaveChangesAsync();

                return city;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
        public async Task<City> DeleteCityAsync(Guid id)
        {
            try
            {
                // Aqui, con el ID que traigo desde el controller, estoy recuperando el pais que luego voy a eliminar
                // ese pais que recupero lo guardo en la variable city
                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
                if (city == null) return null; // si el pais no existe, entonces me retorna un NULL

                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();

                return city;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
    }
}
