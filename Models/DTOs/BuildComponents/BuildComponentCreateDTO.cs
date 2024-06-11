using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildComponentCreateDTO
{
    [Required]
    public int ComponentId { get; set; }

    [Required]
    public int Quantity { get; set; }
}