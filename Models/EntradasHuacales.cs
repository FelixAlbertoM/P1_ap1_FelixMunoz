using System.ComponentModel.DataAnnotations;

namespace P1_ap1_FelixMunoz.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string NombreCliente { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public double Cantidad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public double Precio { get; set; }

}
