using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ComponentController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public ComponentController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<ComponentNoNavDTO> componentDTOs = _dbContext.Components
            .Select(c => new ComponentNoNavDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price
            })
            .ToList();
        
        return Ok(componentDTOs);
    }
}