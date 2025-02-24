namespace CilginMuzisyenler.Models;
using System.ComponentModel.DataAnnotations;

public class Musicians
{
    [Range(1, int.MaxValue, ErrorMessage = "Id alanı 1 ya da daha büyük olmalıdır.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name alanı boş bırakılamaz.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Job alanı boş bırakılamaz.")]
    public string? Job { get; set; }

    public string? FunFact { get; set; }
}