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
            .Include(b => b.CPU)
            .Include(b => b.GPU)
            .Include(b => b.PSU)
            .Include(b => b.Motherboard)
            .Include(b => b.Cooler)
            .Include(b => b.BuildMemories)
            .ThenInclude(bm => bm.Memory)
            .Include(b => b.BuildStorages)
            .ThenInclude(bs => bs.Storage)
            .Include(b => b.Comments)
            .ThenInclude(c => c.Replies)
            .OrderByDescending(b => b.DateCreated)
            .Select(b => new BuildForListDTO()
            {
                Id = b.Id,
                UserProfileId = b.UserProfileId,
                Name = b.Name,
                DateCreated = b.DateCreated,
                Wattage = b.Wattage,
                TotalPrice = b.TotalPrice,
                CommentsQuantity = b.Comments.Sum(c => c.Replies.Count + 1),
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

    [HttpGet("user/{id}")]
    [Authorize]
    public IActionResult GetByUser(int id)
    {
        List<BuildForListDTO> buildDTOs = _dbContext.Builds
            .Where(b => b.UserProfileId == id)
            .Include(b => b.UserProfile)
            .ThenInclude(up => up.IdentityUser)
            .Include(b => b.CPU)
            .Include(b => b.GPU)
            .Include(b => b.PSU)
            .Include(b => b.Motherboard)
            .Include(b => b.Cooler)
            .Include(b => b.BuildMemories)
            .ThenInclude(bm => bm.Memory)
            .Include(b => b.BuildStorages)
            .ThenInclude(bs => bs.Storage)
            .Include(b => b.Comments)
            .ThenInclude(c => c.Replies)
            .OrderByDescending(b => b.DateCreated)
            .Select(b => new BuildForListDTO()
            {
                Id = b.Id,
                UserProfileId = b.UserProfileId,
                Name = b.Name,
                DateCreated = b.DateCreated,
                Wattage = b.Wattage,
                TotalPrice = b.TotalPrice,
                CommentsQuantity = b.Comments.Sum(c => c.Replies.Count + 1),
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
            .Include(b => b.CPU)
            .Include(b => b.GPU)
            .Include(b => b.PSU)
            .Include(b => b.Motherboard)
            .Include(b => b.Cooler)
            .Include(b => b.BuildMemories)
            .ThenInclude(bm => bm.Memory)
            .Include(b => b.BuildStorages)
            .ThenInclude(bs => bs.Storage)
            .Include(b => b.Comments)
            .ThenInclude(c => c.UserProfile)
            .ThenInclude(up => up.IdentityUser)
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
                Wattage = b.Wattage,
                CPUId = b.CPUId,
                CoolerId = b.CoolerId,
                GPUId = b.GPUId,
                MotherboardId = b.MotherboardId,
                PSUId = b.PSUId,
                TotalPrice = b.TotalPrice,
                CPU = new CPUForBuildDTO()
                {
                    Id = b.CPU.Id,
                    Name = b.CPU.Name,
                    TDP = b.CPU.TDP,
                    InterfaceId = b.CPU.InterfaceId,
                    Price = b.CPU.Price
                },
                GPU = new GPUForBuildDTO()
                {
                    Id = b.GPU.Id,
                    Name = b.GPU.Name,
                    TDP = b.GPU.TDP,
                    InterfaceId = b.GPU.InterfaceId,
                    Price = b.GPU.Price
                },
                PSU = new PSUNoNavDTO()
                {
                    Id = b.PSU.Id,
                    Name = b.PSU.Name,
                    Wattage = b.PSU.Wattage,
                    Price = b.PSU.Price
                },
                Cooler = new CoolerNoNavDTO()
                {
                    Id = b.Cooler.Id,
                    Name = b.Cooler.Name,
                    TDP = b.Cooler.TDP,
                    Price = b.Cooler.Price
                },
                Motherboard = new MotherboardForBuildDTO()
                {
                    Id = b.Motherboard.Id,
                    Name = b.Motherboard.Name,
                    Price = b.Motherboard.Price,
                    CPUInterfaceId = b.Motherboard.CPUInterfaceId,
                    GPUInterfaceId = b.Motherboard.GPUInterfaceId,
                    MemoryInterfaceId = b.Motherboard.MemoryInterfaceId,
                    MemorySlots = b.Motherboard.MemorySlots,
                    M2StorageSlots = b.Motherboard.M2StorageSlots,
                    SataStorageSlots = b.Motherboard.SataStorageSlots
                },
                Memory = b.BuildMemories.Select(bm => new MemoryForBuildDetailsDTO()
                {
                    Id = bm.Memory.Id,
                    Name = bm.Memory.Name,
                    SizeGB = bm.Memory.SizeGB,
                    InterfaceId = bm.Memory.InterfaceId,
                    Price = bm.Memory.Price,
                    Quantity = bm.Quantity
                }
                ).ToList(),
                Storage = b.BuildStorages.Select(bs => new StorageForBuildDetailsDTO()
                {
                    Id = bs.Storage.Id,
                    Name = bs.Storage.Name,
                    SizeGB = bs.Storage.SizeGB,
                    InterfaceId = bs.Storage.InterfaceId,
                    Price = bs.Storage.Price,
                    Quantity = bs.Quantity
                }
                ).ToList(),
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
            .Include(b => b.BuildMemories)
            .ThenInclude(bm => bm.Memory)
            .Include(b => b.BuildStorages)
            .ThenInclude(bs => bs.Storage)
            .Select(b => new BuildForEditFormDTO()
            {
                Id = b.Id,
                UserProfileId = b.UserProfileId,
                Name = b.Name,
                Content = b.Content,
                CPUId = b.CPUId,
                CoolerId = b.CoolerId,
                GPUId = b.GPUId,
                MotherboardId = b.MotherboardId,
                PSUId = b.PSUId,
                BuildMemories = b.BuildMemories.Select(bm => new BuildMemoryForEditFormDTO()
                {
                    Id = bm.Memory.Id,
                    Name = bm.Memory.Name,
                    InterfaceId = bm.Memory.InterfaceId,
                    Price = bm.Memory.Price,
                    SizeGB = bm.Memory.SizeGB,
                    Quantity = bm.Quantity
                }
                ).ToList(),
                BuildStorages = b.BuildStorages.Select(bs => new BuildStorageForEditFormDTO()
                {
                    Id = bs.Storage.Id,
                    Name = bs.Storage.Name,
                    InterfaceId = bs.Storage.InterfaceId,
                    Price = bs.Storage.Price,
                    SizeGB = bs.Storage.SizeGB,
                    Quantity = bs.Quantity
                }
                ).ToList(),
                
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
            DateCreated = DateTime.Now,
            CPUId = build.CPUId,
            CoolerId = build.CoolerId,
            GPUId = build.GPUId,
            MotherboardId = build.MotherboardId,
            PSUId = build.PSUId
        };

        _dbContext.Builds.Add(newBuild);
        _dbContext.SaveChanges();

        foreach (BuildMemoryCreateDTO bm in build.BuildMemories)
        {
            BuildMemory buildMemory = new BuildMemory()
            {
                BuildId = newBuild.Id,
                MemoryId = bm.MemoryId,
                Quantity = bm.Quantity
            };

            _dbContext.BuildMemories.Add(buildMemory);
        }

        foreach (BuildStorageCreateDTO bs in build.BuildStorages)
        {
            BuildStorage buildStorage = new BuildStorage()
            {
                BuildId = newBuild.Id,
                StorageId = bs.StorageId,
                Quantity = bs.Quantity
            };

            _dbContext.BuildStorages.Add(buildStorage);
        }

        _dbContext.SaveChanges();

        BuildForBuildDetailsDTO createdBuild = _dbContext.Builds
            .Include(b => b.UserProfile)
            .ThenInclude(up => up.IdentityUser)
            .Include(b => b.CPU)
            .Include(b => b.GPU)
            .Include(b => b.PSU)
            .Include(b => b.Motherboard)
            .Include(b => b.Cooler)
            .Include(b => b.BuildMemories)
            .ThenInclude(bm => bm.Memory)
            .Include(b => b.BuildStorages)
            .ThenInclude(bs => bs.Storage)
            .Select(b => new BuildForBuildDetailsDTO()
            {
                Id = b.Id,
                UserProfileId = b.UserProfileId,
                Name = b.Name,
                Content = b.Content,
                DateCreated = b.DateCreated,
                Wattage = b.Wattage,
                CPUId = b.CPUId,
                CoolerId = b.CoolerId,
                GPUId = b.GPUId,
                MotherboardId = b.MotherboardId,
                PSUId = b.PSUId,
                TotalPrice = b.TotalPrice,
                CPU = new CPUForBuildDTO()
                {
                    Id = b.CPU.Id,
                    Name = b.CPU.Name,
                    TDP = b.CPU.TDP,
                    InterfaceId = b.CPU.InterfaceId,
                    Price = b.CPU.Price
                },
                GPU = new GPUForBuildDTO()
                {
                    Id = b.GPU.Id,
                    Name = b.GPU.Name,
                    TDP = b.GPU.TDP,
                    InterfaceId = b.GPU.InterfaceId,
                    Price = b.GPU.Price
                },
                PSU = new PSUNoNavDTO()
                {
                    Id = b.PSU.Id,
                    Name = b.PSU.Name,
                    Wattage = b.PSU.Wattage,
                    Price = b.PSU.Price
                },
                Cooler = new CoolerNoNavDTO()
                {
                    Id = b.Cooler.Id,
                    Name = b.Cooler.Name,
                    TDP = b.Cooler.TDP,
                    Price = b.Cooler.Price
                },
                Motherboard = new MotherboardForBuildDTO()
                {
                    Id = b.Motherboard.Id,
                    Name = b.Motherboard.Name,
                    Price = b.Motherboard.Price,
                    CPUInterfaceId = b.Motherboard.CPUInterfaceId,
                    GPUInterfaceId = b.Motherboard.GPUInterfaceId,
                    MemoryInterfaceId = b.Motherboard.MemoryInterfaceId,
                    MemorySlots = b.Motherboard.MemorySlots,
                    M2StorageSlots = b.Motherboard.M2StorageSlots,
                    SataStorageSlots = b.Motherboard.SataStorageSlots
                },
                Memory = b.BuildMemories.Select(bm => new MemoryForBuildDetailsDTO()
                {
                    Id = bm.Memory.Id,
                    Name = bm.Memory.Name,
                    SizeGB = bm.Memory.SizeGB,
                    InterfaceId = bm.Memory.InterfaceId,
                    Price = bm.Memory.Price,
                    Quantity = bm.Quantity
                }
                ).ToList(),
                Storage = b.BuildStorages.Select(bs => new StorageForBuildDetailsDTO()
                {
                    Id = bs.Storage.Id,
                    Name = bs.Storage.Name,
                    SizeGB = bs.Storage.SizeGB,
                    InterfaceId = bs.Storage.InterfaceId,
                    Price = bs.Storage.Price,
                    Quantity = bs.Quantity
                }
                ).ToList(),
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
        buildToEdit.CPUId = build.CPUId;
        buildToEdit.PSUId = build.PSUId;
        buildToEdit.GPUId = build.GPUId;
        buildToEdit.CoolerId = build.CoolerId;
        buildToEdit.MotherboardId = build.MotherboardId;

        List<BuildMemory> buildMemoriesToDelete = _dbContext.BuildMemories
            .Where(bm => bm.BuildId == id)
            .ToList();

        foreach (BuildMemory bm in buildMemoriesToDelete)
        {
            _dbContext.BuildMemories.Remove(bm);
        }

        List<BuildStorage> buildStoragesToDelete = _dbContext.BuildStorages
            .Where(bs => bs.BuildId == id)
            .ToList();

        foreach (BuildStorage bs in buildStoragesToDelete)
        {
            _dbContext.BuildStorages.Remove(bs);
        }

        _dbContext.SaveChanges();

        foreach (BuildMemoryCreateDTO bm in build.BuildMemories)
        {
            BuildMemory buildMemory = new BuildMemory()
            {
                BuildId = buildToEdit.Id,
                MemoryId = bm.MemoryId,
                Quantity = bm.Quantity
            };

            _dbContext.BuildMemories.Add(buildMemory);
        }

        foreach (BuildStorageCreateDTO bs in build.BuildStorages)
        {
            BuildStorage buildStorage = new BuildStorage()
            {
                BuildId = buildToEdit.Id,
                StorageId = bs.StorageId,
                Quantity = bs.Quantity
            };

            _dbContext.BuildStorages.Add(buildStorage);
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