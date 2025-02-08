using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Sistemas
    {
        [Key]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "El Campo Descripción es obligatorio ")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Campo Descripción es obligatorio ")]
        public string Complejidad { get; set; }

    }
}
