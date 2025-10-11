using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P1_ap1_FelixMunoz.Models;

public class EntradasHuacalesDetalle
{
    [Key]
    public int detalleId { get; set; }

    public int IdEntrada { get; set; }

    public int IdTipo { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }


    [ForeignKey("IdEntrada")]
    [InverseProperty("DetalleHuacales")]
    public virtual EntradasHuacales EntradaHuacal { get; set; }  = null!;

    [ForeignKey("IdTipo")]
    [InverseProperty("EntradasHuacalesDetalle")]
    public virtual TiposHuacales TipoHuacal { get; set; }  = null!;
}
