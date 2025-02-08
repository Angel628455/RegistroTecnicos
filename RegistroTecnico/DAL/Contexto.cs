using RegistroTecnico.Models;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnico.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Tecnicos> Tecnico { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Ciudades> Ciudades { get; set; }
    public DbSet<Tickets> Tickets { get; internal set; }
}