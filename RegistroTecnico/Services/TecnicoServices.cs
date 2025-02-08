using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;
public class TecnicoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnico.AnyAsync(t => t.TecnicoId == id);
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnico.Add(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnico.Update(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Tecnicos = await contexto.Tecnico
            .Where(t => t.TecnicoId == id).ExecuteDeleteAsync();
        return Tecnicos > 0;
    }
    public async Task<Tecnicos?> Buscar(int id)
    {
        // Crear el contexto con using para asegurar su disposición adecuada.
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // Realizar la consulta y devolver el resultado.
        return await contexto.Tecnico.AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }

    public async Task<Tecnicos?> BuscarNombres(string nombre)
    {
        // Crear el contexto con using para asegurar su disposición adecuada.
        await using var contexto = await DbFactory.CreateDbContextAsync();

        // Realizar la consulta y devolver el resultado.
        return await contexto.Tecnico.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Nombre == nombre);
    }
    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnico.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}