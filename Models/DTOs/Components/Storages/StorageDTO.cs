using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class StorageDTO
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
    public InterfaceDTO Interface { get; set; }
    
    public List<BuildStorageDTO> BuildStorages { get; set; }
}