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

    // Calculated Properties
    public decimal TotalPrice => Components.Sum(c => c.Price * c.Quantity);

    public int ComponentsQuantity => Components.Sum(c => c.Quantity);
    
    public string FormattedDateCreated => DateCreated.ToString("MMMM dd, yyyy");

    // Navigation Properties
    public UserProfileForBuildDTO UserProfile { get; set; }

    public List<ComponentForBuildDTO> Components { get; set; }

    public List<CommentForBuildDTO> Comments { get; set; }
}