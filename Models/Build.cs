using System.ComponentModel.DataAnnotations;

namespace CPYou.Models;

public class Build
{
    public int Id { get; set; }
    
    [Required]
    public int UserProfileId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public DateTime DateCreated { get; set; }

    [Required]
    public int CPUId { get; set; }

    [Required]
    public int CoolerId { get; set; }

    [Required]
    public int GPUId { get; set; }

    [Required]
    public int MotherboardId { get; set; }

    [Required]
    public int PSUId { get; set; }

    // Calculated Properties
    public decimal TotalPrice
    {
        get
        {
            try
            {
                decimal CPUPrice = CPU.Price;
                decimal GPUPrice = GPU.Price;
                decimal PSUPrice = PSU.Price;
                decimal CoolerPrice = Cooler.Price;
                decimal MotherboardPrice = Motherboard.Price;
                decimal MemoryPrice = BuildMemories.Sum(bm => bm.Memory.Price * bm.Quantity);
                decimal StoragePrice = BuildStorages.Sum(bs => bs.Storage.Price * bs.Quantity);

                decimal totalPrice = CPUPrice + GPUPrice + PSUPrice + CoolerPrice + MotherboardPrice + MemoryPrice + StoragePrice;

                return totalPrice;
            }
            catch
            {
                return 0M;
            }
            
        }
    }

    public int Wattage
    {
        get
        {
            try
            {
                return (int)Math.Ceiling((CPU.TDP + GPU.TDP) * 1.2);
            }
            catch
            {
                return 0;
            }
        }
    }

    // Navigation Properties
    public UserProfile UserProfile { get; set; }

    public List<Comment> Comments { get; set; }

    public CPU CPU { get; set; }

    public Cooler Cooler { get; set; }

    public GPU GPU { get; set; }

    public Motherboard Motherboard { get; set; }

    public PSU PSU { get; set; }

    public List<BuildMemory> BuildMemories { get; set; }

    public List<BuildStorage> BuildStorages { get; set; }
}