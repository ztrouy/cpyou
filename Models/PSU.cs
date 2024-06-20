using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class PSU
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Wattage { get; set; }

    // Navigation Properties
    public List<Build> Builds { get; set; }
}