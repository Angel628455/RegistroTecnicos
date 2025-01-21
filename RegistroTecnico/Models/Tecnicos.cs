using System.ComponentModel.DataAnnotations;
namespace RegistroTecnico.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]

        public string Nombre { get; internal set; }

        public decimal SueldoHora { get; set; }
    }
}
