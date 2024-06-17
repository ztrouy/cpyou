using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class ReplyUpdateDTO
{
    public int Id { get; set; }
    
    [Required]
    public string Content { get; set; }
}