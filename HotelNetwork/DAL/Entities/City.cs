using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class State : AuditBase
    {
        [Display(Name = "Ciudad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name { get; set; }

        [Display(Name = "Id País")]
        public Guid StateId { get; set; }    //FK
    }
}
}
