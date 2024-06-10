using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BuildController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public BuildController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        List<BuildForListDTO> buildDTOs = _dbContext.Builds
            .Include(b => b.UserProfile)
            .ThenInclude(up => up.IdentityUser)
            .Include(b => b.BuildComponents)
            .ThenInclude(bc => bc.Component)
            .Select(b => new BuildForListDTO()
            {
                Id = b.Id,
                UserProfileId = b.UserProfileId,
                Name = b.Name,
                DateCreated = b.DateCreated,
                TotalPrice = b.BuildComponents.Sum(bc => bc.Component.Price * bc.Quantity),
                ComponentsQuantity = b.BuildComponents.Sum(bc => bc.Quantity),
                UserProfile = new UserProfileForBuildDTO()
                {
                    Id = b.UserProfile.Id,
                    FirstName = b.UserProfile.FirstName,
                    LastName = b.UserProfile.LastName,
                    UserName = b.UserProfile.IdentityUser.UserName,
                    ImageLocation = b.UserProfile.ImageLocation
                }
            })
            .ToList();
        
        return Ok(buildDTOs);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetSingle(int id)
    {
        BuildForBuildDetailsDTO buildDTO = _dbContext.Builds
            .Include(b => b.UserProfile)
            .ThenInclude(up => up.IdentityUser)
            .Include(b => b.BuildComponents)
            .ThenInclude(bc => bc.Component)
            .Select(b => new BuildForBuildDetailsDTO()
            {
                Id = b.Id,
                UserProfileId = b.UserProfileId,
                Name = b.Name,
                Content = b.Content,
                DateCreated = b.DateCreated,
                Components = b.BuildComponents.Select(bc => new ComponentForBuildDTO()
                {
                    Id = bc.Component.Id,
                    Name = bc.Component.Name,
                    Price = bc.Component.Price,
                    Quantity = bc.Quantity
                }).ToList(),
                UserProfile = new UserProfileForBuildDTO()
                {
                    Id = b.UserProfile.Id,
                    FirstName = b.UserProfile.FirstName,
                    LastName = b.UserProfile.LastName,
                    UserName = b.UserProfile.IdentityUser.UserName,
                    ImageLocation = b.UserProfile.ImageLocation
                }
            })
            .SingleOrDefault(b => b.Id == id);
        
        return Ok(buildDTO);
    }
}