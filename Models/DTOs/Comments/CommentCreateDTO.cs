using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class CommentCreateDTO
{
    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public int BuildId { get; set; }

    [Required]
    public string Content { get; set; }
}