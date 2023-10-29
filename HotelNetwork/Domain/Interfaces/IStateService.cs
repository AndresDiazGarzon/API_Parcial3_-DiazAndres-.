using HotelNetwork.DAL.Entities;

namespace HotelNetwork.Domain.Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetStatesAsync();// una firma de metodo    
        Task<State> CreateStateAsync(State state);
        Task<State> GetStateByIdAsync(Guid id);
        Task<State> GetStateByNameAsync(string name);
        Task<State> EditStateAsync(State state);
        Task<State> DeleteStateAsync(Guid Id);
    }
}
