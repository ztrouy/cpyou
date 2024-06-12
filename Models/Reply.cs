using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Reply
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
    public Comment Comment { get; set; }

    public UserProfile UserProfile { get; set; }
}