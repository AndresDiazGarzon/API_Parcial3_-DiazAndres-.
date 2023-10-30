using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class State : AuditBase
    {
        [Display(Name = "Estado/Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name { get; set; }

        [Display(Name = "País")]
        //Relación con Country
        public Country? Country { get; set; } //Este representa un OBJETO DE COUNTRY

        [Display(Name = "Id País")]
        public Guid CountryId { get; set; } //FK
        public override Guid Id { get => base.Id; init => base.Id = value; }
        public override DateTime? CreateDate { get => base.CreateDate; init => base.CreateDate = value; }
        public override DateTime? ModifiedDate { get => base.ModifiedDate; init => base.ModifiedDate = value; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
