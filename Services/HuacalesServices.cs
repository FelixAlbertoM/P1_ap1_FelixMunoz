using Microsoft.EntityFrameworkCore;
using P1_ap1_FelixMunoz.DAL;
using P1_ap1_FelixMunoz.Models;
using System.Linq.Expressions;

namespace P1_ap1_FelixMunoz.Services;

public class HuacalesService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.AnyAsync(e => e.IdEntrada == idEntrada);
    }

    private async Task<bool> Insertar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.EntradasHuacales.Add(entrada);
        await AfectarExistencia(entrada.DetalleHuacales.ToArray(), TipoOperacion.Resta);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var anterior = await contexto.EntradasHuacales
            .Include(e => e.DetalleHuacales)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.IdEntrada == entrada.IdEntrada);

        if (anterior != null)
        {
            await AfectarExistencia(anterior.DetalleHuacales.ToArray(), TipoOperacion.Resta);
        }

        await AfectarExistencia(entrada.DetalleHuacales.ToArray(), TipoOperacion.Suma);

        contexto.Update(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task AfectarExistencia(EntradasHuacalesDetalle[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var item in detalle)
        {
            var tipo = await contexto.TiposHuacales.SingleAsync(t => t.IdTipo == item.IdTipo);

            if (tipoOperacion == TipoOperacion.Resta)
                tipo.Existencia -= item.Cantidad;
            else
                tipo.Existencia += item.Cantidad;
        }

        await contexto.SaveChangesAsync();
    }

    public async Task<bool> Guardar(EntradasHuacales entrada)
    {
        if (!await Existe(entrada.IdEntrada))
        {
            return await Insertar(entrada);
        }
        else
        {
            return await Modificar(entrada);
        }
    }

    public async Task<EntradasHuacales?> Buscar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Include(e => e. DetalleHuacales)
            .FirstOrDefaultAsync(e => e.IdEntrada == idEntrada);
    }

    public async Task<bool> Eliminar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entrada = await contexto.EntradasHuacales
            .Include(e => e.DetalleHuacales)
            .FirstOrDefaultAsync(e => e.IdEntrada == idEntrada);

        if (entrada == null) return false;

        await AfectarExistencia(entrada.DetalleHuacales.ToArray(), TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalle.RemoveRange(entrada.DetalleHuacales);
        contexto.EntradasHuacales.Remove(entrada);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Include(e => e.DetalleHuacales)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}

