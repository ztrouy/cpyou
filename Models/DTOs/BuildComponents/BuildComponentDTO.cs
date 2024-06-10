using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildComponentDTO
{
    [Required]
    public int BuildId { get; set; }

    [Required]
    public int ComponentId { get; set; }

    [Required]
    public int Quantity { get; set; }

    // Navigation Properties
    public BuildDTO Build { get; set; }
    public ComponentDTO Component { get; set; }
}