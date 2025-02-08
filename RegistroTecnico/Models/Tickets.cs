using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Tickets
    {
        [Key]
        public int TicketId { get; set; }
        public DateTime Fecha { get; set; }
        public string Prioridad { get; set; }
        public int ClienteId { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public double TiempoInvertido { get; set; }
        public int TecnicoId { get; set; }
    }
}