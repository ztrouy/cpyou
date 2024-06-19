using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public CommentController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet("{id}/home")]
    [Authorize]
    public IActionResult GetForHomeView(int id)
    {
        List<CommentForHomeDTO> commentDTOs = _dbContext.Comments
            .Include(c => c.UserProfile)
            .ThenInclude(up => up.IdentityUser)
            .Include(c => c.Replies)
            .ThenInclude(r => r.UserProfile)
            .ThenInclude(up => up.IdentityUser)
            .Include(c => c.Build)
            .Where(c => 
                ((c.UserProfileId != id) & (c.Build.UserProfileId == id) & (c.Replies.OrderBy(r => r.Id).LastOrDefault().UserProfileId != id)) |
                ((c.UserProfileId == id) & (c.Replies.Count > 0) & (c.Replies.OrderBy(r => r.Id).LastOrDefault().UserProfileId != id)) |
                ((c.Replies.Any(r => r.UserProfileId == id)) & (c.Replies.OrderBy(r => r.Id).LastOrDefault().UserProfileId != id))
            )
            .Select(c => new CommentForHomeDTO()
            {
                Id = c.Id,
                UserProfileId = c.UserProfileId,
                BuildId = c.BuildId,
                BuildName = c.Build.Name,
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
            })
            .OrderByDescending(c => c.Replies.Count == 0 ? (c.DateCreated) : (c.Replies.Last().DateCreated))
            .Take(3)
            .ToList();

        return Ok(commentDTOs);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CommentCreateDTO comment)
    {
        Build foundBuild = _dbContext.Builds.SingleOrDefault(b => b.Id == comment.BuildId);
        if (foundBuild == null)
        {
            return BadRequest("Cannot find the Build you are commenting on!");
        }

        UserProfile foundUserProfile = _dbContext.UserProfiles.SingleOrDefault(up => up.Id == comment.UserProfileId);
        if (foundUserProfile == null)
        {
            return BadRequest("Cannot find the User who is commenting!");
        }

        Comment newComment = new Comment()
        {
            BuildId = comment.BuildId,
            UserProfileId = comment.UserProfileId,
            Content = comment.Content,
            DateCreated = DateTime.Now
        };

        _dbContext.Comments.Add(newComment);
        _dbContext.SaveChanges();

        CommentForBuildDTO createdComment = _dbContext.Comments
            .Include(c => c.UserProfile)
            .Select(c => new CommentForBuildDTO()
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
                }
            }).SingleOrDefault(c => c.Id == newComment.Id);
        
        return Created($"/builds/{createdComment.BuildId}", createdComment);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, CommentUpdateDTO comment)
    {
        Comment commentToUpdate = _dbContext.Comments.SingleOrDefault(c => c.Id == id);
        if (commentToUpdate == null)
        {
            return NotFound("Comment not found");
        }

        commentToUpdate.Content = comment.Content;

        _dbContext.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        Comment commentToDelete = _dbContext.Comments.SingleOrDefault(c => c.Id == id);
        if (commentToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Comments.Remove(commentToDelete);

        _dbContext.SaveChanges();

        return NoContent();
    }
}