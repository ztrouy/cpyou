using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Motherboard
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
    public Interface CPUInterface { get; set; }

    public Interface GPUInterface { get; set; }

    public Interface MemoryInterface { get; set; }

    public List<Build> Builds { get; set; }
}