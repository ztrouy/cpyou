using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class InterfaceDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation Properties
    public List<CPUDTO> CPUs { get; set; }

    public List<GPUDTO> GPUs { get; set; }

    public List<MemoryDTO> Memories { get; set; }

    public List<StorageDTO> Storages { get; set; }

    public List<MotherboardDTO> Motherboards { get; set; }
}