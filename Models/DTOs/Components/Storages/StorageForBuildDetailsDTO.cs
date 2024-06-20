using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class StorageForBuildDetailsDTO
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

    [Required]
    public int Quantity { get; set; }
}