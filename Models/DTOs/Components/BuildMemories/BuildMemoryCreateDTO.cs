using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildMemoryCreateDTO
{
    [Required]
    public int MemoryId { get; set; }

    [Required]
    public int Quantity { get; set; }
}