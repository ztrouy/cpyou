using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class PSUNoNavDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Wattage { get; set; }
}