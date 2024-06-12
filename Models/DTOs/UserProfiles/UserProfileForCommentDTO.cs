namespace CPYou.Models.DTOs;
public class UserProfileForCommentDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string ImageLocation { get; set; }

    // Calculated Properties
    public string FullName => $"{FirstName} {LastName}";
}