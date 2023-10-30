using DocumentFormat.OpenXml.Bibliography;
using HotelNetwork.DAL.Entities;

namespace HotelNetwork.Domain.Interfaces
{
    public interface ICityService   
    {
        Task<IEnumerable<City>> GetCitysByCityIdAsync(Guid cityId);
        Task<City> CreateCityAsync(City city, Guid countryId);
        Task<City> GetCityByIdAsync(Guid id);
        Task<City> EditCityAsync(City city, Guid id);
        Task<City> DeleteCityAsync(Guid id);
        Task GetCitysByStateIdAsync(Guid stateId);
    }
}
