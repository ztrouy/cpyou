using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PSUController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public PSUController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<PSUNoNavDTO> psuDTOs = _dbContext.PSUs
            .Select(c => new PSUNoNavDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Wattage = c.Wattage,
                Price = c.Price
            })
            .ToList();
        
        return Ok(psuDTOs);
    }
}