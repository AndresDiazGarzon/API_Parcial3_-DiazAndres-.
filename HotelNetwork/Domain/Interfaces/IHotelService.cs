using HotelNetwork.DAL.Entities;

namespace HotelNetwork.Domain.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<State>> GetHotelAsync();// una firma de metodo    
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> GetHoteleByIdAsync(Guid id);
        Task<Hotel> GetHotelByNameAsync(string name);
        Task<Hotel> EditHotelAsync(Hotel hotel);
        Task<Hotel> DeleteHotelAsync(Guid Id);
    }
}
}
