using HotelNetwork.DAL.Entities;

namespace HotelNetwork.Domain.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<State>> GetCitiesAsync();// una firma de metodo    
        Task<City> CreateStateAsync(City city);
        Task<City> GetCityByIdAsync(Guid id);
        Task<City> GetCityByNameAsync(string name);
        Task<City> EditCityAsync(State state);
        Task<City> DeleteCityAsync(Guid Id);
    }
}
