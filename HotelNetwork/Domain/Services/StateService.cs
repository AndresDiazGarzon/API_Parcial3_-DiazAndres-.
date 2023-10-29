using HotelNetwork.DAL.Entities;
using HotelNetwork.DAL;

namespace HotelNetwork.Domain.Services
{
    public class StateService
    {
        private readonly DataBaseContext _context;
        public StateService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            return await _context.States.ToListAsync();

        }
        public async Task<State> CreateStateAsync(State state)
        {
            try
            {
                state.Id = Guid.NewGuid();// asi se asigna automaticamente un ID a un nuevo registro
                state.CreateDate = DateTime.Now;

                _context.States.Add(state);//Aqui estoy creado el objedo State en el contexto de mi BD
                await _context.SaveChangesAsync();// Aqui ya estoy yendo a la BD para hacer el INSERT en la tabla Countries 

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                // Esta exception no captura un mensaje cuando el pais YA EXISTE (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?

            }

        }

        public async Task<State> GetStateByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); // FindAsyn es un metodo propio del DbContext (Dbset)
            //return await _context.Countries.FirstAsync(x => x.Id == id);// FirstAsync es un metodo de EF CORE
            return await _context.States.FirstOrDefaultAsync(c => c.Id == id); // FirstOrDefaultAsync es un metodo de EF CORE
        }

        public async Task<State> GetStateByNameAsync(string name)
        {
            return await _context.States.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<State> EditStateAsync(State state)
        {
            try
            {
                state.ModifiedDate = DateTime.Now;

                _context.Countries.Update(state);//El metodo Update que es de EF CORE me sirve para actualizar un objeto
                await _context.SaveChangesAsync();

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
        public async Task<State> DeleteStateAsync(Guid id)
        {
            try
            {
                // Aqui, con el ID que traigo desde el controller, estoy recuperando el estado que luego voy a eliminar
                // ese pais que recupero lo guardo en la variable estado
                var state = await _context.States.FirstOrDefaultAsync(c => c.Id == id);
                if (state == null) return null; // si el estado no existe, entonces me retorna un NULL

                _context.States.Remove(state);
                await _context.SaveChangesAsync();

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);// Coallesences Notation --> ?
            }
        }
    }
}
