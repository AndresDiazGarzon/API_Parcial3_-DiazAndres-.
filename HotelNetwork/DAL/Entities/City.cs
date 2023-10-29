using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class City
    {
        [Display(Name = "Ciudad ")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]

        public string Name { get; set; }
        [Display(Name = "Estado")]

        
        public State? State { get; set; }// este representa un objeto de COUNTRY

        [Display(Name = "Id Estado")]
        public Guid StateId  { get; set; }// FK
    }
}
