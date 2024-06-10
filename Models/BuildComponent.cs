using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class BuildComponent
{
    [Required]
    public int BuildId { get; set; }

    [Required]
    public int ComponentId { get; set; }

    [Required]
    public int Quantity { get; set; }

    // Navigation Properties
    public Build Build { get; set; }
    
    public Component Component { get; set; }
}