using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildStorageForBuildDTO
{
    [Required]
    public int BuildId { get; set; }

    [Required]
    public int StorageId { get; set; }

    [Required]
    public int Quantity { get; set; }

    // Navigation Properties
    public StorageDTO Storage { get; set; }
}