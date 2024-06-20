using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class InterfaceNoNavDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}