using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CoolerController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public CoolerController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<CoolerNoNavDTO> coolerDTOs = _dbContext.Coolers
            .Select(c => new CoolerNoNavDTO()
            {
                Id = c.Id,
                Name = c.Name,
                TDP = c.TDP,
                Price = c.Price
            })
            .ToList();
        
        return Ok(coolerDTOs);
    }
}