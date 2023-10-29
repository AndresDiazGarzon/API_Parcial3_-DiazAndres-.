using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class Country : AuditBase
    {
        [Display(Name = "País")] // Para yo pintar el nombre bien bonito en el FrontEnd
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")] //Longitud de caracteres máxima que esta propiedad me permite tener, ejem varchar(50)
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name { get; set; } //varchar(50)

        [Display(Name = "Estados")]
        //relación con State 
        public ICollection<State>? States { get; set; }
    }
}
