using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CPUController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public CPUController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<CPUNoNavDTO> cpuDTOs = _dbContext.CPUs
            .Select(c => new CPUNoNavDTO()
            {
                Id = c.Id,
                Name = c.Name,
                InterfaceId = c.InterfaceId,
                TDP = c.TDP,
                Price = c.Price
            })
            .ToList();
        
        return Ok(cpuDTOs);
    }
}