using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Nombres { get; set; }
        public string Direccion { get; set; }
        public string RNC { get; set; }
        public decimal LimiteCredito { get; set; }
        [ForeignKey("Tecnico")]
        public int TecnicoId { get; set; }

    }
}

