using Microsoft.EntityFrameworkCore;
using P1_ap1_FelixMunoz.DAL;
using P1_ap1_FelixMunoz.Models;
using System.Linq.Expressions;

namespace P1_ap1_FelixMunoz.Services;

public class HuacalesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(EntradasHuacales entradasHuacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Entry(entradasHuacales).State = entradasHuacales.IdEntrada == 0 ? EntityState.Added : EntityState.Modified;

        return await contexto.SaveChangesAsync() > 0;

    }
    public async Task<bool> Existe(int IdEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.AnyAsync(e => e.IdEntrada == IdEntrada);
    }

    public async Task<bool> ExisteNombre(string nombre, int empleadoId = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AnyAsync(e => e.NombreCliente == nombre && e.IdEntrada != empleadoId);
    }

    public async Task<bool> Insertar(EntradasHuacales entradasHuacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.EntradasHuacales.Add(entradasHuacales);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(EntradasHuacales entradasHuacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(entradasHuacales);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<EntradasHuacales?> Buscar(int IdEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.FirstOrDefaultAsync(e => e.IdEntrada == IdEntrada);
    }

    public async Task<bool> Eliminar(int IdEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.AsNoTracking().Where(e => e.IdEntrada == IdEntrada).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.Where(criterio).AsNoTracking().ToListAsync();
    }

}