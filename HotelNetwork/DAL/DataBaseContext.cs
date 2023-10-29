using HotelNetwork.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelNetwork.DAL
{
    public class DataBaseContext : DbContext
    {
        internal object Cities;

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) 
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c=> c.Name).IsUnique();//Esto es un indice para evitar nombres duplicados
            modelBuilder.Entity<State>().HasIndex("Name","CountryId").IsUnique();// Indices Compuestos
            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex("Name", "CityId").IsUnique();
            modelBuilder.Entity<Room>().HasIndex("Name", "HotelId").IsUnique();

        }
        public DbSet<Country> Countries { get; set; }// esta linea me toma la clase Country y me la mapea en SQL SERVER para crear una tabla llamada COUNTRIES
        
        // por cada nueva entidad que yo creo , debo crearle su DbSet

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }    
        public DbSet<Room> Rooms { get; set; } 
       
    }
}
