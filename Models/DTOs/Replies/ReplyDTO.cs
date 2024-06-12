using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class ReplyDTO
{
    public int Id { get; set; }
    
    [Required]
    public int CommentId { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }

    // Navigation Properties
    public CommentDTO Comment { get; set; }

    public UserProfileForReplyDTO UserProfile { get; set; }
}