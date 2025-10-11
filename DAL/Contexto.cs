using Microsoft.EntityFrameworkCore;
using P1_ap1_FelixMunoz.Models;

namespace P1_ap1_FelixMunoz.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<EntradasHuacales> EntradasHuacales { get; set; }
    public DbSet<TiposHuacales> TiposHuacales { get; set; }
    public DbSet<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposHuacales>().HasData(
            new List<TiposHuacales>()
            {
                new()
                {
                    IdTipo = 1,
                    Descripcion = "Huacal Rojo - Grande",
                    Existencia = 20
                },
                new()
                {
                    IdTipo = 2,
                    Descripcion = "Huacal verde - Grande",
                    Existencia = 20
                },
                new()
                {
                    IdTipo = 3,
                    Descripcion = "Huacal verde - Pequeña",
                    Existencia = 20
                },
                 new()
                {
                    IdTipo = 4,
                    Descripcion = "Huacal Rojo - Pequeña",
                    Existencia = 20,
                },
            }
        );
        base.OnModelCreating(modelBuilder);
    }

}

