using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class ComponentNoNavDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }
}