using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildEditDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int UserProfileId { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public int CPUId { get; set; }

    [Required]
    public int CoolerId { get; set; }

    [Required]
    public int GPUId { get; set; }

    [Required]
    public int MotherboardId { get; set; }

    [Required]
    public int PSUId { get; set; }

    [Required]
    public List<BuildMemoryCreateDTO> BuildMemories { get; set; }

    [Required]
    public List<BuildStorageCreateDTO> BuildStorages { get; set; }
}