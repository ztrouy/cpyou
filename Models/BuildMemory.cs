using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class BuildMemory
{
    [Required]
    public int BuildId { get; set; }

    [Required]
    public int MemoryId { get; set; }

    [Required]
    public int Quantity { get; set; }

    // Navigation Properties
    public Build Build { get; set; }

    public Memory Memory { get; set; }
}