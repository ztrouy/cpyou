using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class StorageNoNavDTO
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

    // Calculated Properties
    public string FullName
    {
        get
        {
            if (SizeGB < 1000)
            {
                return $"{Name} {SizeGB}GB";
            } else {
                return $"{Name} {Math.Round((decimal)(SizeGB / 1000), 1)}TB";
            }
        }
    }
}