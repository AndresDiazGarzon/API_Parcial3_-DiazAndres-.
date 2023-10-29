using HotelNetwork.DAL.Entities;

namespace HotelNetwork.Domain.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCitiesAsync();// una firma de metodo    
        Task<City> CreateCityAsync(City city);
        Task<City> GetCityByIdAsync(Guid id);
        Task<City> GetCityByNameAsync(string name);
        Task<City> EditCityAsync(City city);    
        Task<City> DeleteCityAsync(Guid Id);
    }
}
