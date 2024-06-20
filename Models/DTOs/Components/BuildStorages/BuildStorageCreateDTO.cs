using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildStorageCreateDTO
{
    [Required]
    public int StorageId { get; set; }

    [Required]
    public int Quantity { get; set; }
}