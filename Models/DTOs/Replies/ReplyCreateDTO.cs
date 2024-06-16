using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class ReplyCreateDTO
{
    [Required]
    public int CommentId { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public string Content { get; set; }
}