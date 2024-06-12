using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CPYou.Models;
using Microsoft.AspNetCore.Identity;

namespace CPYou.Data;
public class CPYouDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Build> Builds { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<BuildComponent> BuildComponents { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reply> Replies { get; set; }


    public CPYouDbContext(DbContextOptions<CPYouDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BuildComponent>().HasKey(bc => new { bc.BuildId, bc.ComponentId });
        modelBuilder.Entity<BuildComponent>()
            .HasOne(bc => bc.Build)
            .WithMany(b => b.BuildComponents)
            .HasForeignKey(bc => bc.BuildId);
        modelBuilder.Entity<BuildComponent>()
            .HasOne(bc => bc.Component)
            .WithMany(b => b.BuildComponents)
            .HasForeignKey(bc => bc.ComponentId);

        
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser[]
        {
            new IdentityUser
            {
                Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                UserName = "Administrator",
                Email = "admina@strator.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                UserName = "JohnDoe",
                Email = "john@doe.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "a7d21fac-3b21-454a-a747-075f072d0cf3",
                UserName = "JaneSmith",
                Email = "jane@smith.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
                UserName = "AliceJohnson",
                Email = "alice@johnson.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
                UserName = "BobWilliams",
                Email = "bob@williams.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
                UserName = "EveDavis",
                Email = "Eve@Davis.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },

        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
            },
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df"
            },

        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile[]
        {
            new UserProfile
            {
                Id = 1,
                IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                FirstName = "Admina",
                LastName = "Strator",
                ImageLocation = "https://robohash.org/numquamutut.png?size=150x150&set=set1",
                DateCreated = new DateTime(2022, 1, 25)
            },
             new UserProfile
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                DateCreated = new DateTime(2023, 2, 2),
                ImageLocation = "https://robohash.org/nisiautemet.png?size=150x150&set=set1",
                IdentityUserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df"
            },
            new UserProfile
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Smith",
                DateCreated = new DateTime(2022, 3, 15),
                ImageLocation = "https://robohash.org/molestiaemagnamet.png?size=150x150&set=set1",
                IdentityUserId = "a7d21fac-3b21-454a-a747-075f072d0cf3"
            },
            new UserProfile
            {
                Id = 4,
                FirstName = "Alice",
                LastName = "Johnson",
                DateCreated = new DateTime(2023, 6, 10),
                ImageLocation = "https://robohash.org/deseruntutipsum.png?size=150x150&set=set1",
                IdentityUserId = "c806cfae-bda9-47c5-8473-dd52fd056a9b"
            },
            new UserProfile
            {
                Id = 5,
                FirstName = "Bob",
                LastName = "Williams",
                DateCreated = new DateTime(2023, 5, 15),
                ImageLocation = "https://robohash.org/quiundedignissimos.png?size=150x150&set=set1",
                IdentityUserId = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2"
            },
            new UserProfile
            {
                Id = 6,
                FirstName = "Eve",
                LastName = "Davis",
                DateCreated = new DateTime(2022, 10, 18),
                ImageLocation = "https://robohash.org/hicnihilipsa.png?size=150x150&set=set1",
                IdentityUserId = "d224a03d-bf0c-4a05-b728-e3521e45d74d"
            }
        });

        // Seed data for Components
        modelBuilder.Entity<Component>().HasData(new Component[]
        {
            new Component { Id = 1, Name = "Intel Core i9-11900K", Price = 499.99m },
            new Component { Id = 2, Name = "AMD Ryzen 9 5900X", Price = 549.99m },
            new Component { Id = 3, Name = "NVIDIA GeForce RTX 3080", Price = 699.99m },
            new Component { Id = 4, Name = "Corsair Vengeance LPX 16GB", Price = 79.99m },
            new Component { Id = 5, Name = "Samsung 970 EVO 1TB SSD", Price = 129.99m },
            new Component { Id = 6, Name = "ASUS ROG Strix Z590-E", Price = 379.99m },
            new Component { Id = 7, Name = "Cooler Master MasterLiquid ML360R", Price = 129.99m },
            new Component { Id = 8, Name = "EVGA SuperNOVA 750W PSU", Price = 109.99m },
            new Component { Id = 9, Name = "Fractal Design Meshify C", Price = 99.99m },
            new Component { Id = 10, Name = "MSI MAG B550 TOMAHAWK", Price = 179.99m },
            new Component { Id = 11, Name = "Crucial Ballistix 32GB", Price = 149.99m },
            new Component { Id = 12, Name = "Western Digital Black 2TB HDD", Price = 89.99m },
            new Component { Id = 13, Name = "Noctua NH-D15", Price = 89.99m },
            new Component { Id = 14, Name = "Gigabyte AORUS Master X570", Price = 349.99m },
            new Component { Id = 15, Name = "NZXT Kraken Z73", Price = 249.99m }
        });

        // Seed data for Builds
        modelBuilder.Entity<Build>().HasData(new Build[]
        {
            new Build
            {
                Id = 1,
                UserProfileId = 1,
                Name = "Gaming Rig 1",
                Content = "High-end gaming PC with Intel and NVIDIA",
                DateCreated = new DateTime(2023, 1, 1)
            },
            new Build
            {
                Id = 2,
                UserProfileId = 2,
                Name = "Workstation",
                Content = "Workstation build for content creation",
                DateCreated = new DateTime(2023, 2, 1)
            },
            new Build
            {
                Id = 3,
                UserProfileId = 3,
                Name = "Budget Build",
                Content = "Affordable build for everyday use",
                DateCreated = new DateTime(2023, 3, 1)
            },
            new Build
            {
                Id = 4,
                UserProfileId = 4,
                Name = "Streaming PC",
                Content = "PC optimized for streaming and gaming",
                DateCreated = new DateTime(2023, 4, 1)
            },
            new Build
            {
                Id = 5,
                UserProfileId = 5,
                Name = "VR Ready Build",
                Content = "Powerful build for VR gaming",
                DateCreated = new DateTime(2023, 5, 1)
            },
            new Build
            {
                Id = 6,
                UserProfileId = 6,
                Name = "Mini ITX Build",
                Content = "Compact build for small spaces",
                DateCreated = new DateTime(2023, 6, 1)
            },

        });

        // Seed data for BuildComponents
        modelBuilder.Entity<BuildComponent>().HasData(new BuildComponent[]
        {
            // Build 1 Components
            new BuildComponent { BuildId = 1, ComponentId = 1, Quantity = 1 },
            new BuildComponent { BuildId = 1, ComponentId = 3, Quantity = 1 },
            new BuildComponent { BuildId = 1, ComponentId = 5, Quantity = 1 },
            new BuildComponent { BuildId = 1, ComponentId = 6, Quantity = 1 },
            new BuildComponent { BuildId = 1, ComponentId = 8, Quantity = 1 },
            new BuildComponent { BuildId = 1, ComponentId = 9, Quantity = 1 },
            new BuildComponent { BuildId = 1, ComponentId = 11, Quantity = 2 },
            new BuildComponent { BuildId = 1, ComponentId = 12, Quantity = 1 },
            new BuildComponent { BuildId = 1, ComponentId = 13, Quantity = 1 },

            // Build 2 Components
            new BuildComponent { BuildId = 2, ComponentId = 2, Quantity = 1 },
            new BuildComponent { BuildId = 2, ComponentId = 3, Quantity = 1 },
            new BuildComponent { BuildId = 2, ComponentId = 4, Quantity = 4 },
            new BuildComponent { BuildId = 2, ComponentId = 5, Quantity = 1 },
            new BuildComponent { BuildId = 2, ComponentId = 14, Quantity = 1 },
            new BuildComponent { BuildId = 2, ComponentId = 15, Quantity = 1 },

            // Build 3 Components
            new BuildComponent { BuildId = 3, ComponentId = 1, Quantity = 1 },
            new BuildComponent { BuildId = 3, ComponentId = 3, Quantity = 1 },
            new BuildComponent { BuildId = 3, ComponentId = 4, Quantity = 2 },
            new BuildComponent { BuildId = 3, ComponentId = 5, Quantity = 1 },
            new BuildComponent { BuildId = 3, ComponentId = 6, Quantity = 1 },
            new BuildComponent { BuildId = 3, ComponentId = 8, Quantity = 1 },
            new BuildComponent { BuildId = 3, ComponentId = 12, Quantity = 1 },

            // Build 4 Components
            new BuildComponent { BuildId = 4, ComponentId = 2, Quantity = 1 },
            new BuildComponent { BuildId = 4, ComponentId = 3, Quantity = 1 },
            new BuildComponent { BuildId = 4, ComponentId = 4, Quantity = 4 },
            new BuildComponent { BuildId = 4, ComponentId = 5, Quantity = 1 },
            new BuildComponent { BuildId = 4, ComponentId = 13, Quantity = 1 },
            new BuildComponent { BuildId = 4, ComponentId = 14, Quantity = 1 },

            // Build 5 Components
            new BuildComponent { BuildId = 5, ComponentId = 1, Quantity = 1 },
            new BuildComponent { BuildId = 5, ComponentId = 2, Quantity = 1 },
            new BuildComponent { BuildId = 5, ComponentId = 3, Quantity = 1 },
            new BuildComponent { BuildId = 5, ComponentId = 4, Quantity = 4 },
            new BuildComponent { BuildId = 5, ComponentId = 5, Quantity = 2 },
            new BuildComponent { BuildId = 5, ComponentId = 6, Quantity = 1 },
            new BuildComponent { BuildId = 5, ComponentId = 8, Quantity = 1 },

            // Build 6 Components
            new BuildComponent { BuildId = 6, ComponentId = 1, Quantity = 1 },
            new BuildComponent { BuildId = 6, ComponentId = 5, Quantity = 1 },
            new BuildComponent { BuildId = 6, ComponentId = 8, Quantity = 1 },
            new BuildComponent { BuildId = 6, ComponentId = 9, Quantity = 1 },
            new BuildComponent { BuildId = 6, ComponentId = 11, Quantity = 2 },
            new BuildComponent { BuildId = 6, ComponentId = 13, Quantity = 1 },
        });

        // Seed data for Comments
        modelBuilder.Entity<Comment>().HasData(new Comment[]
        {
            new Comment { Id = 1, UserProfileId = 1, BuildId = 5, Content = "VR ready and under budget, nice!", DateCreated = new DateTime(2023, 5, 25) },
            new Comment { Id = 2, UserProfileId = 2, BuildId = 1, Content = "This build looks amazing! How does it handle AAA games?", DateCreated = new DateTime(2023, 1, 5) },
            new Comment { Id = 3, UserProfileId = 2, BuildId = 1, Content = "Great build!", DateCreated = DateTime.Now },
            new Comment { Id = 4, UserProfileId = 2, BuildId = 6, Content = "How does it perform?", DateCreated = DateTime.Now },
            new Comment { Id = 5, UserProfileId = 3, BuildId = 1, Content = "I have a similar setup. Runs like a dream!", DateCreated = new DateTime(2023, 1, 6) },
            new Comment { Id = 6, UserProfileId = 3, BuildId = 2, Content = "Very efficient setup.", DateCreated = DateTime.Now },
            new Comment { Id = 7, UserProfileId = 4, BuildId = 2, Content = "Great choice for content creation. The Ryzen 9 is a beast.", DateCreated = new DateTime(2023, 2, 10) },
            new Comment { Id = 8, UserProfileId = 4, BuildId = 3, Content = "Looks good for the price.", DateCreated = DateTime.Now },
            new Comment { Id = 9, UserProfileId = 5, BuildId = 3, Content = "Perfect budget build. Thanks for sharing!", DateCreated = new DateTime(2023, 3, 15) },
            new Comment { Id = 10, UserProfileId = 5, BuildId = 4, Content = "I'm thinking of building something similar.", DateCreated = DateTime.Now },
            new Comment { Id = 11, UserProfileId = 6, BuildId = 4, Content = "Thinking of building something similar for streaming. Any tips?", DateCreated = new DateTime(2023, 4, 20) },
            new Comment { Id = 12, UserProfileId = 6, BuildId = 5, Content = "Any issues with streaming?", DateCreated = DateTime.Now }
        });

        // Seed data for Replies
        modelBuilder.Entity<Reply>().HasData(new Reply[]
        {
            new Reply { Id = 1, CommentId = 1, UserProfileId = 5, Content = "Thanks! It's been great so far.", DateCreated = DateTime.Now },
            new Reply { Id = 2, CommentId = 1, UserProfileId = 1, Content = "What VR hardware are you using?", DateCreated = DateTime.Now },
            new Reply { Id = 3, CommentId = 2, UserProfileId = 2, Content = "Yes, it runs smoothly.", DateCreated = DateTime.Now },
            new Reply { Id = 4, CommentId = 2, UserProfileId = 5, Content = "What software do you use?", DateCreated = DateTime.Now },
            new Reply { Id = 5, CommentId = 3, UserProfileId = 6, Content = "I'm considering this build too.", DateCreated = DateTime.Now },
            new Reply { Id = 6, CommentId = 3, UserProfileId = 2, Content = "It's quite good for the budget.", DateCreated = DateTime.Now },
            new Reply { Id = 7, CommentId = 4, UserProfileId = 3, Content = "You should go for it!", DateCreated = DateTime.Now },
            new Reply { Id = 8, CommentId = 4, UserProfileId = 6, Content = "What components are you considering?", DateCreated = DateTime.Now }
        });
    }
}