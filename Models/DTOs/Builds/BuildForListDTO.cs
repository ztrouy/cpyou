using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildForListDTO
{
    public int Id { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }

    public decimal TotalPrice { get; set; }

    public int ComponentsQuantity { get; set; }

    // Calculated Properties
    public string FormattedDateCreated => DateCreated.ToString("MMMM dd, yyyy");

    // Navigation Properties
    public UserProfileForBuildDTO UserProfile { get; set; }
}