using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildStorageDTO
{
    [Required]
    public int BuildId { get; set; }

    [Required]
    public int StorageId { get; set; }

    [Required]
    public int Quantity { get; set; }

    // Navigation Properties
    public BuildDTO Build { get; set; }

    public StorageDTO Storage { get; set; }
}