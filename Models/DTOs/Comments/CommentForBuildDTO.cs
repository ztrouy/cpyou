using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class CommentForBuildDTO
{
    public int Id { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public int BuildId { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }

    // Calculated Properties
    public string FormattedDateCreated => DateCreated.ToString("MMM dd, yyyy");

    // Navigation Properties
    public UserProfileForCommentDTO UserProfile { get; set; }

    public List<ReplyForCommentDTO> Replies { get; set; }
}