using DocumentFormat.OpenXml.Bibliography;
using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class Hotel
    {
        [Display(Name = "Hotel ")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]

        public string Name { get; set; }
        [Display(Name = "Pais")]

        
        public City? City { get; set; }// este representa un objeto de 

        [Display(Name = "Id Ciudad")]
        public Guid CiudadId { get; set; }// FK
        public DateTime CreateDate { get; internal set; }
        public Guid Id { get; internal set; }
        public DateTime ModifiedDate { get; internal set; }
    }
}
