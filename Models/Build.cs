using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Build
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

    // Navigation Properties
    public UserProfile UserProfile { get; set; }
    public List<BuildComponent> BuildComponents { get; set; }
}