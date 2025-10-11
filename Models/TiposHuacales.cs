using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P1_ap1_FelixMunoz.Models;

public class TiposHuacales
{
    [Key]
    public int IdTipo { get; set; }


    [StringLength(100)]
    public string Descripcion { get; set; } = string.Empty;

    public int Existencia { get; set; }

    [InverseProperty("TipoHuacal")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; } =  new List<EntradasHuacalesDetalle>();
}
