using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using RegistroTecnico.DAL;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class TicketServices
    {
        private readonly Contexto _contexto;

        public TicketServices(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int TicketId)
        {
            return await _contexto.Tickets.AnyAsync(t => t.TicketId == TicketId);
        }

        private async Task<bool> Insertar(Tickets ticket)
        {
            _contexto.Tickets.Add(ticket);
            return await _contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tickets ticket)
        {
            _contexto.Tickets.Update(ticket);
            var modificado = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(ticket).State = EntityState.Detached;
            return modificado;
        }

        public async Task<bool> Guardar(Tickets ticket)
        {
            if (!await Existe(ticket.TicketId))
            {
                return await Insertar(ticket);
            }
            else
            {
                return await Modificar(ticket);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var eliminado = await _contexto.Tickets.Where(t => t.TicketId == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<Tickets?> Buscar(int id)
        {
            return await _contexto.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.TicketId == id);
        }

        public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> Criterio)
        {
            return await _contexto.Tickets
            .Where(Criterio)
            .ToListAsync();
        }
    }

}