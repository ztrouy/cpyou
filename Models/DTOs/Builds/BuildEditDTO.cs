using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildEditDTO
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public List<BuildComponentCreateDTO> Components { get; set; }
}