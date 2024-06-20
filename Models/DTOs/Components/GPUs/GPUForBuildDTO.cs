using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class GPUForBuildDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int InterfaceId { get; set; }

    [Required]
    public int TDP { get; set; }

    // Navigation Properties
    public InterfaceNoNavDTO Interface { get; set; }
}