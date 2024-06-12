using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Comment
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

    // Navigation Properties
    public UserProfile UserProfile { get; set; }

    public Build Build { get; set; }

    public List<Reply> Replies { get; set; }
}