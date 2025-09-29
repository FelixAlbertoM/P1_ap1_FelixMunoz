using Microsoft.EntityFrameworkCore;
using P1_ap1_FelixMunoz.DAL;
using P1_ap1_FelixMunoz.Models;
using System.Linq.Expressions;

namespace P1_ap1_FelixMunoz.Services;

public class Servicios(IDbContextFactory<Contexto> contextFactory)
{

    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Registros.Where(criterio).AsNoTracking().ToListAsync();
    }

}