using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class GPUController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public GPUController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<GPUNoNavDTO> gpuDTOs = _dbContext.GPUs
            .Select(g => new GPUNoNavDTO()
            {
                Id = g.Id,
                Name = g.Name,
                InterfaceId = g.InterfaceId,
                TDP = g.TDP,
                Price = g.Price
            })
            .ToList();
        
        return Ok(gpuDTOs);
    }
}