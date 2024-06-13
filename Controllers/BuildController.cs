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
            .OrderByDescending(b => b.DateCreated)
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
            .Include(b => b.Comments)
            .ThenInclude(c => c.UserProfile)
            .Include(b => b.Comments)
            .ThenInclude(c => c.Replies)
            .ThenInclude(r => r.UserProfile)
            .ThenInclude(up => up.IdentityUser)
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
                },
                Comments = b.Comments.Select(c => new CommentForBuildDTO()
                {
                    Id = c.Id,
                    UserProfileId = c.UserProfileId,
                    BuildId = c.BuildId,
                    Content = c.Content,
                    DateCreated = c.DateCreated,
                    UserProfile = new UserProfileForCommentDTO()
                    {
                        Id = c.UserProfile.Id,
                        FirstName = c.UserProfile.FirstName,
                        LastName = c.UserProfile.LastName,
                        UserName = c.UserProfile.IdentityUser.UserName,
                        ImageLocation = c.UserProfile.ImageLocation
                    },
                    Replies = c.Replies.Select(r => new ReplyForCommentDTO()
                    {
                        Id = r.Id,
                        CommentId = r.CommentId,
                        UserProfileId = r.UserProfileId,
                        Content = r.Content,
                        DateCreated = r.DateCreated,
                        UserProfile = new UserProfileForReplyDTO()
                        {
                            Id = r.UserProfile.Id,
                            FirstName = r.UserProfile.FirstName,
                            LastName = r.UserProfile.LastName,
                            ImageLocation = r.UserProfile.ImageLocation,
                            UserName = r.UserProfile.IdentityUser.UserName
                        }
                    }).OrderBy(r => r.DateCreated).ToList()
                }).OrderByDescending(c => c.DateCreated).ToList()
            })
            .SingleOrDefault(b => b.Id == id);
        
        if (buildDTO == null)
        {
            return NotFound();
        }

        return Ok(buildDTO);
    }

    [HttpGet("{id}/edit")]
    [Authorize]
    public IActionResult GetSingleForEdit(int id)
    {
        BuildForEditFormDTO buildDTO = _dbContext.Builds
            .Include(b => b.UserProfile)
            .Include(b => b.BuildComponents)
            .ThenInclude(bc => bc.Component)
            .Select(b => new BuildForEditFormDTO()
            {
                Id = b.Id,
                UserProfileId = b.UserProfileId,
                Name = b.Name,
                Content = b.Content,
                Components = b.BuildComponents.Select(bc => new BuildComponentForEditFormDTO()
                {
                    Id = bc.ComponentId,
                    Name = bc.Component.Name,
                    Price = bc.Component.Price,
                    Quantity = bc.Quantity
                }).ToList()
                
            })
            .SingleOrDefault(b => b.Id == id);
        
        if (buildDTO == null)
        {
            return NotFound();
        }

        return Ok(buildDTO);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(BuildCreateDTO build)
    {
        UserProfile foundUserProfile = _dbContext.UserProfiles.SingleOrDefault(up => up.Id == build.UserProfileId);
        if (foundUserProfile == null)
        {
            return BadRequest("No User exists with that Id!");
        }
        
        Build newBuild = new Build()
        {
            Name = build.Name,
            Content = build.Content,
            UserProfileId = build.UserProfileId,
            DateCreated = DateTime.Now
        };

        _dbContext.Builds.Add(newBuild);
        _dbContext.SaveChanges();

        foreach (BuildComponentCreateDTO bc in build.Components)
        {
            BuildComponent buildComponent = new BuildComponent()
            {
                BuildId = newBuild.Id,
                ComponentId = bc.ComponentId,
                Quantity = bc.Quantity
            };

            _dbContext.BuildComponents.Add(buildComponent);
        }

        _dbContext.SaveChanges();

        BuildForBuildDetailsDTO createdBuild = _dbContext.Builds
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
            .SingleOrDefault(b => b.Id == newBuild.Id);
        
        return Created($"/builds/{createdBuild.Id}", createdBuild);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, BuildEditDTO build)
    {
        Build buildToEdit = _dbContext.Builds.SingleOrDefault(b => b.Id == id);
        if (buildToEdit == null)
        {
            return BadRequest("No Build exists with that Id!");
        }
        
        buildToEdit.Name = build.Name;
        buildToEdit.Content = build.Content;

        List<BuildComponent> buildComponentsToDelete = _dbContext.BuildComponents
            .Where(bc => bc.BuildId == id)
            .ToList();

        foreach (BuildComponent bc in buildComponentsToDelete)
        {
            _dbContext.BuildComponents.Remove(bc);
        }

        _dbContext.SaveChanges();

        foreach (BuildComponentCreateDTO bc in build.Components)
        {
            BuildComponent buildComponent = new BuildComponent()
            {
                BuildId = id,
                ComponentId = bc.ComponentId,
                Quantity = bc.Quantity
            };

            _dbContext.BuildComponents.Add(buildComponent);
        }

        _dbContext.SaveChanges();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        Build buildToDelete = _dbContext.Builds.SingleOrDefault(b => b.Id == id);
        if (buildToDelete == null)
        {
            return NotFound("No Build found with that Id");
        }

        _dbContext.Builds.Remove(buildToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }
}