using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MotherboardController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public MotherboardController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<MotherboardNoNavDTO> motherboardDTOs = _dbContext.Motherboards
            .Select(m => new MotherboardNoNavDTO()
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                CPUInterfaceId = m.CPUInterfaceId,
                GPUInterfaceId = m.GPUInterfaceId,
                MemoryInterfaceId = m.MemoryInterfaceId,
                MemorySlots = m.MemorySlots,
                M2StorageSlots = m.M2StorageSlots,
                SataStorageSlots = m.SataStorageSlots
            })
            .ToList();
        
        return Ok(motherboardDTOs);
    }
}