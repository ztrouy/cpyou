using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildForEditFormDTO
{
    public int Id { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Content { get; set; }

    // Navigation Properties
    public List<BuildComponentForEditFormDTO> Components { get; set; }
}