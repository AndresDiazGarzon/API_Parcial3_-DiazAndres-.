using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class Country: AuditBase
    {
        [Display(Name = "Pais")]// para yo pintar el nombre bien bonito en el FrontEnd
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]// Longitud de caractres maxima que
        // esta propiedad me permite tener, ejem varch(50)
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name {  get; set; }// varchar(50)
        [Display (Name = "Estado")]
        // relacion con State
        public ICollection<State>? States { get; set;}

        public ICollection<City>? Cities{ get; set;}

        public ICollection<Hotel>? Hotels  { get; set; }

        public ICollection<Room>? Rooms { get; set; }
       
    }
}
