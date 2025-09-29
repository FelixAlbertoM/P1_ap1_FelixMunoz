using System.ComponentModel.DataAnnotations;

namespace P1_ap1_FelixMunoz.Models;

public class EntradasHuacales
{
    [Key]
    public int RegistroId { get; set; }

    [Required(ErrorMessage = "El campo Registro es obligatorio.")]
    public string Registro { get; set; } = null;

}
