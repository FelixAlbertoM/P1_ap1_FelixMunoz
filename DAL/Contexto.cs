using Microsoft.EntityFrameworkCore;
using P1_ap1_FelixMunoz.Models;

namespace P1_ap1_FelixMunoz.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<EntradasHuacales> EntradasHuacales { get; set; }

}

