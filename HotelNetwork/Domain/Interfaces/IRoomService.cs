using HotelNetwork.DAL.Entities;

namespace HotelNetwork.Domain.Interfaces    
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomAsync();// una firma de metodo    
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> GetRoomeByIdAsync(Guid id);
        Task<Room> GetRoomByNameAsync(string name);
        Task<Room> EditRoomAsync(Room room);
        Task<Room> DeleteRoomAsync(Guid Id);
        Task<Room> GetRoomByIdAsync(Guid id);
        Task<Room> GetRoomsAsync();
    }
}
