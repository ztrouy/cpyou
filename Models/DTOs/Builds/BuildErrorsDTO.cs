using System.ComponentModel.DataAnnotations;

namespace CPYou.Models.DTOs;

public class BuildErrorsDTO
{    
    public List<string> CPU { get; set; } = new List<string>();

    public List<string> Cooler { get; set; } = new List<string>();

    public List<string> GPU { get; set; } = new List<string>();

    public List<string> Motherboard { get; set; } = new List<string>();

    public List<string> PSU { get; set; } = new List<string>();

    public List<string> Memory { get; set; } = new List<string>();

    public List<string> Storage { get; set; } = new List<string>();
}