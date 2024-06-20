using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Interface
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation Properties
    public List<CPU> CPUs { get; set; }

    public List<GPU> GPUs { get; set; }

    public List<Memory> Memories { get; set; }

    public List<Storage> Storages { get; set; }

    public List<Motherboard> CPUMotherboards { get; set; }

    public List<Motherboard> GPUMotherboards { get; set; }

    public List<Motherboard> MemoryMotherboards { get; set; }
}