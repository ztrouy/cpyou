using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StorageController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public StorageController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<StorageNoNavDTO> storageDTOs = _dbContext.Storages
            .Select(s => new StorageNoNavDTO()
            {
                Id = s.Id,
                Name = s.Name,
                InterfaceId = s.InterfaceId,
                SizeGB = s.SizeGB,
                Price = s.Price
            })
            .ToList();
        
        return Ok(storageDTOs);
    }
}