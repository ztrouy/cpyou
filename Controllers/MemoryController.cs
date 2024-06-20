using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MemoryController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public MemoryController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<MemoryNoNavDTO> memoryDTOs = _dbContext.Memories
            .Select(m => new MemoryNoNavDTO()
            {
                Id = m.Id,
                Name = m.Name,
                InterfaceId = m.InterfaceId,
                SizeGB = m.SizeGB,
                Price = m.Price
            })
            .ToList();
        
        return Ok(memoryDTOs);
    }
}