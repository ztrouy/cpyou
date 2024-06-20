using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildMemoryForBuildDTO
{
    [Required]
    public int BuildId { get; set; }

    [Required]
    public int MemoryId { get; set; }

    [Required]
    public int Quantity { get; set; }

    // Navigation Properties
    public MemoryDTO Memory { get; set; }
}