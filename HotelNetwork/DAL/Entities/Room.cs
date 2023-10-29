using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class Room
    {
        [Display(Name = "Habitacion ")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]

        public string Name { get; set; }
        [Display(Name = "Habitacion")]

        
        public Hotel? Hotel { get; set; }// este representa un objeto 

        [Display(Name = "Id Hotel")]
        public Guid HotelId { get; set; }// FK
        public DateTime CreateDate { get; internal set; }
    }
}
