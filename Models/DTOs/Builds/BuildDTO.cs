using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildDTO
{
    public int Id { get; set; }
    
    [Required]
    public int UserProfileId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public DateTime DateCreated { get; set; }

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

    public decimal TotalPrice { get; set; }

    public int Wattage { get; set; }

    // Calculated Properties    
    public string FormattedDateCreated => DateCreated.ToString("MMMM dd, yyyy");

    // Navigation Properties
    public UserProfileForBuildDTO UserProfile { get; set; }

    public List<CommentForBuildDTO> Comments { get; set; }

    public CPUDTO CPU { get; set; }

    public CoolerDTO Cooler { get; set; }

    public GPUDTO GPU { get; set; }

    public MotherboardDTO Motherboard { get; set; }

    public PSUDTO PSU { get; set; }

    public List<BuildMemoryDTO> BuildMemories { get; set; }

    public List<BuildStorageDTO> BuildStorages { get; set; }
}