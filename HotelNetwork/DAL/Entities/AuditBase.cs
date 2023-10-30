using System.ComponentModel.DataAnnotations;

namespace HotelNetwork.DAL.Entities
{
    public class AuditBase
    {
        [Key]// DataAnnotation me sirve para definir que esta propiedad ID es un PK
        [Required]// para campos obligatorios, no permite nulls
        public virtual Guid Id { get; init; }// sera el PK de todas las tablas de mi BD
        public virtual DateTime? CreateDate { get; init; }// campos nulleables, notacon elvis (?)
        public virtual DateTime? ModifiedDate { get; init; }       

        [Display(Name = "Ciudad")]
        //Relación con 
        public State? State { get; set; } //Este representa un OBJETO 
    }
}
