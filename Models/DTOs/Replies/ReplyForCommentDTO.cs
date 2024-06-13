using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class ReplyForCommentDTO
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

    // Calculated Properties
    public string FormattedDateCreated => DateCreated.ToString("MMM dd, yyyy");

    // Navigation Properties
    public UserProfileForReplyDTO UserProfile { get; set; }
}