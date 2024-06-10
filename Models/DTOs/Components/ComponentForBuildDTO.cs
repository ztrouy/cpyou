using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class ComponentForBuildDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    public int Quantity { get; set; }
}