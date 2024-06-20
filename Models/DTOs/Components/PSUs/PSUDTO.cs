using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class PSUDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Wattage { get; set; }

    // Navigation Properties
    public List<BuildDTO> Builds { get; set; }
}