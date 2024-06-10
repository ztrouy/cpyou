using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Component
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    // Navigation Properties
    public List<BuildComponent> BuildComponents { get; set; }
}