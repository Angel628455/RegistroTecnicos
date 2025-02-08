using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;
public class CiudadesServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.AnyAsync(c => c.CiudadId == id);
    }

    private async Task<bool> Insertar(Ciudades ciudad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Ciudades.Add(ciudad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Ciudades ciudad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Ciudades.Update(ciudad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Ciudades ciudad)
    {
        if (!await Existe(ciudad.CiudadId))
            return await Insertar(ciudad);
        else
            return await Modificar(ciudad);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var ciudadesEliminadas = await contexto.Ciudades
            .Where(c => c.CiudadId == id).ExecuteDeleteAsync();
        return ciudadesEliminadas > 0;
    }

    public async Task<Ciudades?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.AsNoTracking()
            .FirstOrDefaultAsync(c => c.CiudadId == id);
    }

    public async Task<Ciudades?> BuscarNombre(string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nombre == nombre);
    }

    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
