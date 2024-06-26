using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CPYou.Models.DTOs;
public class UserProfileDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [NotMapped]
    public string UserName { get; set; }

    [NotMapped]
    public string Email { get; set; }

    public DateTime DateCreated { get; set; }

    [DataType(DataType.Url)]
    [MaxLength(255)]
    public string ImageLocation { get; set; }

    [NotMapped]
    public List<string> Roles { get; set; }

    public string IdentityUserId { get; set; }

    // Calculated Properties
    public string FullName => $"{FirstName} {LastName}";

    // Navigation Properties
    public IdentityUser IdentityUser { get; set; }

    public List<BuildDTO> Builds { get; set; }

    public List<CommentDTO> Comments { get; set; }

    public List<ReplyDTO> Replies { get; set; }
}