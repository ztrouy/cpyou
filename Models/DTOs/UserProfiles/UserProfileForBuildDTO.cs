using Microsoft.AspNetCore.Identity;

namespace CPYou.Models.DTOs;
public class UserProfileForBuildDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string ImageLocation { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}