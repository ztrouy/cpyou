using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class BuildStorage
{
    [Required]
    public int BuildId { get; set; }

    [Required]
    public int StorageId { get; set; }

    [Required]
    public int Quantity { get; set; }

    // Navigation Properties
    public Build Build { get; set; }

    public Storage Storage { get; set; }
}