using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using HotelNetwork.DAL;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;

namespace HotelNetwork.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataBaseContext _context;
        public CountryService(DataBaseContext context)
        {
            _context = context; 
        }
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return  await _context.Countries.ToListAsync();// Aqui lo que hago es traerme todos los datos
            //tengo en mi tabla Countries
            
        }
        public async Task<Country> CreateCountryAsync(Country  country)
        {
            try 
            {
                country.Id = Guid.NewGuid();// asi se asigna automaticamente un ID a un nuevo registro
                country.CreateDate = DateTime.Now;

                _context.Countries.Add(country);//Aqui estoy creado el objedo Country en el contexto de mi BD
                await _context.SaveChangesAsync();// Aqui ya estoy yendo a la BD para hacer el INSERT en la tabla Countries 

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                // Esta exception no captura un mensaje cuando el pais YA EXISTE (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
                
            }

        }

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); // FindAsyn es un metodo propio del DbContext (Dbset)
            //return await _context.Countries.FirstAsync(x => x.Id == id);// FirstAsync es un metodo de EF CORE
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id); // FirstOrDefaultAsync es un metodo de EF CORE
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Country> EditCountryAsync(Country country)
        {
            try
            {
                country.ModifiedDate = DateTime.Now;

                _context.Countries.Update(country);//El metodo Update que es de EF CORE me sirve para actualizar un objeto
                await _context.SaveChangesAsync();

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            try
            {
                // Aqui, con el ID que traigo desde el controller, estoy recuperando el pais que luego voy a eliminar
                // ese pais que recupero lo guardo en la variable country
                var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (country == null) return null; // si el pais no existe, entonces me retorna un NULL

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
    }
}
