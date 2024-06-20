using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class MotherboardDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CPUInterfaceId { get; set; }

    [Required]
    public int GPUInterfaceId { get; set; }

    [Required]
    public int MemoryInterfaceId { get; set; }

    [Required]
    public int MemorySlots { get; set; }

    [Required]
    public int M2StorageSlots { get; set; }

    [Required]
    public int SataStorageSlots { get; set; }

    // Navigation Properties
    public InterfaceDTO CPUInterface { get; set; }

    public InterfaceDTO MemoryInterface { get; set; }

    public InterfaceDTO GPUInterface { get; set; }

    public List<BuildDTO> Builds { get; set; }
}