using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Memory
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int InterfaceId { get; set; }

    [Required]
    public int SizeGB { get; set; }

    // Navigation Properties
    public Interface Interface { get; set; }
    
    public List<BuildMemory> BuildMemories { get; set; }
}