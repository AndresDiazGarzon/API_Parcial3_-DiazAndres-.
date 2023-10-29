using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class State
    {
        [Display(Name = "Estado/Departamento ")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]

        public string Name { get; set; }
        [Display (Name = "Pais")]

        // relacion con Country
        public Country? Country { get; set; }// este representa un objeto de COUNTRY

        [Display(Name = "Id País")]
        public Guid CountryId { get; set; }// FK
        public DateTime CreateDate { get; internal set; }
    }
}
