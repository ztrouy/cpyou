using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildForBuildDetailsDTO
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

    public CPUForBuildDTO CPU { get; set; }

    public CoolerNoNavDTO Cooler { get; set; }

    public GPUForBuildDTO GPU { get; set; }

    public MotherboardForBuildDTO Motherboard { get; set; }

    public PSUNoNavDTO PSU { get; set; }

    public List<MemoryForBuildDetailsDTO> Memory { get; set; }

    public List<StorageForBuildDetailsDTO> Storage { get; set; }
}