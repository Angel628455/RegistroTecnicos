using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class ClienteServices
    {
        private readonly Contexto _contexto;

        public ClienteServices(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int ClienteId)
        {
            return await _contexto.Clientes.AnyAsync(t => t.ClienteId == ClienteId);
        }

        private async Task<bool> Insertar(Clientes cliente)
        {
            _contexto.Clientes.Add(cliente);
            return await _contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Clientes cliente)
        {
            _contexto.Clientes.Update(cliente);
            var modificado = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(cliente).State = EntityState.Detached;
            return modificado;
        }

        public async Task<bool> Guardar(Clientes cliente)
        {
            if (!await Existe(cliente.ClienteId))
            {
                return await Insertar(cliente);
            }
            else
            {
                return await Modificar(cliente);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var eliminado = await _contexto.Clientes.Where(t => t.ClienteId == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<Clientes?> Buscar(int id)
        {
            return await _contexto.Clientes.AsNoTracking().FirstOrDefaultAsync(t => t.ClienteId == id);
        }

        public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> Criterio)
        {
            return await _contexto.Clientes
            .Where(Criterio)
            .ToListAsync();
        }

    }

}