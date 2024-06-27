using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class InterfaceController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public InterfaceController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<InterfaceNoNavDTO> interfaceDTOs = _dbContext.Interfaces
            .Select(c => new InterfaceNoNavDTO()
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
        
        return Ok(interfaceDTOs);
    }
}