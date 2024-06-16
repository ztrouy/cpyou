using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPYou.Data;
using Microsoft.EntityFrameworkCore;
using CPYou.Models;
using CPYou.Models.DTOs;

namespace CPYou.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ReplyController : ControllerBase
{
    private CPYouDbContext _dbContext;

    public ReplyController(CPYouDbContext context)
    {
        _dbContext = context;
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(ReplyCreateDTO reply)
    {
        Comment foundComment = _dbContext.Comments.SingleOrDefault(c => c.Id == reply.CommentId);
        if (foundComment == null)
        {
            return BadRequest("Cannot find the Comment you are replying to!");
        }

        UserProfile foundUserProfile = _dbContext.UserProfiles.SingleOrDefault(up => up.Id == reply.UserProfileId);
        if (foundUserProfile == null)
        {
            return BadRequest("Cannot find the User who is commenting!");
        }

        Reply newReply = new Reply()
        {
            CommentId = reply.CommentId,
            UserProfileId = reply.UserProfileId,
            Content = reply.Content,
            DateCreated = DateTime.Now
        };

        _dbContext.Replies.Add(newReply);
        _dbContext.SaveChanges();

        ReplyForCommentDTO createdReply = _dbContext.Replies
            .Include(c => c.UserProfile)
            .Select(c => new ReplyForCommentDTO()
            {
                Id = c.Id,
                UserProfileId = c.UserProfileId,
                CommentId = c.CommentId,
                Content = c.Content,
                DateCreated = c.DateCreated,
                UserProfile = new UserProfileForReplyDTO()
                {
                    Id = c.UserProfile.Id,
                    FirstName = c.UserProfile.FirstName,
                    LastName = c.UserProfile.LastName,
                    UserName = c.UserProfile.IdentityUser.UserName,
                    ImageLocation = c.UserProfile.ImageLocation
                }
            }).SingleOrDefault(r => r.Id == newReply.Id);
        
        return Created($"/builds/{foundComment.BuildId}", createdReply);
    }
}