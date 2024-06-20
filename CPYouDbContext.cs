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
    public DbSet<CPU> CPUs { get; set; }
    public DbSet<GPU> GPUs { get; set; }
    public DbSet<PSU> PSUs { get; set; }
    public DbSet<Motherboard> Motherboards { get; set; }
    public DbSet<Memory> Memories { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<BuildMemory> BuildMemories { get; set; }
    public DbSet<BuildStorage> BuildStorages { get; set; }
    public DbSet<Cooler> Coolers { get; set; }
    public DbSet<Interface> Interfaces { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reply> Replies { get; set; }


    public CPYouDbContext(DbContextOptions<CPYouDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BuildMemory>().HasKey(bc => new { bc.BuildId, bc.MemoryId });
        modelBuilder.Entity<BuildMemory>()
            .HasOne(bc => bc.Build)
            .WithMany(b => b.BuildMemories)
            .HasForeignKey(bc => bc.BuildId);
        modelBuilder.Entity<BuildMemory>()
            .HasOne(bc => bc.Memory)
            .WithMany(b => b.BuildMemories)
            .HasForeignKey(bc => bc.MemoryId);

        modelBuilder.Entity<BuildStorage>().HasKey(bc => new { bc.BuildId, bc.StorageId });
        modelBuilder.Entity<BuildStorage>()
            .HasOne(bc => bc.Build)
            .WithMany(b => b.BuildStorages)
            .HasForeignKey(bc => bc.BuildId);
        modelBuilder.Entity<BuildStorage>()
            .HasOne(bc => bc.Storage)
            .WithMany(b => b.BuildStorages)
            .HasForeignKey(bc => bc.StorageId);

        modelBuilder.Entity<Motherboard>()
            .HasOne(m => m.CPUInterface)
            .WithMany(i => i.CPUMotherboards)
            .HasForeignKey(m => m.CPUInterfaceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Motherboard>()
            .HasOne(m => m.GPUInterface)
            .WithMany(i => i.GPUMotherboards)
            .HasForeignKey(m => m.GPUInterfaceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Motherboard>()
            .HasOne(m => m.MemoryInterface)
            .WithMany(i => i.MemoryMotherboards)
            .HasForeignKey(m => m.MemoryInterfaceId)
            .OnDelete(DeleteBehavior.Restrict);

        
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

        // Seed data for Interfaces
        modelBuilder.Entity<Interface>().HasData(new Interface[]
        {
            new Interface { Id = 1, Name = "AM5" },
            new Interface { Id = 2, Name = "AM4" },
            new Interface { Id = 3, Name = "LGA1700" },
            new Interface { Id = 4, Name = "DDR5 DIMM" },
            new Interface { Id = 5, Name = "DDR4 DIMM" },
            new Interface { Id = 6, Name = "PCIe x16" },
            new Interface { Id = 7, Name = "SATA" },
            new Interface { Id = 8, Name = "M.2 NVMe" },
        });

        // Seed data for CPUs
        modelBuilder.Entity<CPU>().HasData(new CPU[]
        {
            new CPU { Id = 1, Name = "Ryzen 9 7950X", InterfaceId = 1, TDP = 170, Price = 486.99M },
            new CPU { Id = 2, Name = "Ryzen 9 7950X3D", InterfaceId = 1, TDP = 120, Price = 479.99M },
            new CPU { Id = 3, Name = "Ryzen 9 7900X", InterfaceId = 1, TDP = 170, Price = 365.99M },
            new CPU { Id = 4, Name = "Ryzen 9 7900X3D", InterfaceId = 1, TDP = 120, Price = 394.99M },
            new CPU { Id = 5, Name = "Ryzen 7 7800X3D", InterfaceId = 1, TDP = 120, Price = 339.99M },
            new CPU { Id = 6, Name = "Ryzen 7 7700X", InterfaceId = 1, TDP = 105, Price = 275.99M },
            new CPU { Id = 7, Name = "Ryzen 5 7600X", InterfaceId = 1, TDP = 105, Price = 197.99M },
            new CPU { Id = 8, Name = "Ryzen 9 5950X", InterfaceId = 2, TDP = 105, Price = 355.00M },
            new CPU { Id = 9, Name = "Ryzen 9 5900X", InterfaceId = 2, TDP = 105, Price = 249.00M },
            new CPU { Id = 10, Name = "Ryzen 7 5800X", InterfaceId = 2, TDP = 105, Price = 177.99M },
            new CPU { Id = 11, Name = "Ryzen 7 5800X3D", InterfaceId = 2, TDP = 105, Price = 199.99M },
            new CPU { Id = 12, Name = "Ryzen 7 5700X", InterfaceId = 2, TDP = 65, Price = 163.99M },
            new CPU { Id = 13, Name = "Ryzen 7 5700X3D", InterfaceId = 2, TDP = 65, Price = 199.99M },
            new CPU { Id = 14, Name = "Ryzen 5 5600X", InterfaceId = 2, TDP = 65, Price = 134.85M },
            new CPU { Id = 15, Name = "Ryzen 9 3950X", InterfaceId = 2, TDP = 65, Price = 249.99M },
            new CPU { Id = 16, Name = "Ryzen 9 3900X", InterfaceId = 2, TDP = 65, Price = 174.99M },
            new CPU { Id = 17, Name = "Ryzen 7 3800X", InterfaceId = 2, TDP = 65, Price = 99.99M },
            new CPU { Id = 18, Name = "Ryzen 7 3700X", InterfaceId = 2, TDP = 65, Price = 89.99M },
            new CPU { Id = 19, Name = "Ryzen 5 3600X", InterfaceId = 2, TDP = 65, Price = 74.99M },
            new CPU { Id = 20, Name = "Ryzen 3 3300X", InterfaceId = 2, TDP = 65, Price = 67.99M },
            new CPU { Id = 21, Name = "Core i9-14900K", InterfaceId = 3, TDP = 253, Price = 546.98M },
            new CPU { Id = 22, Name = "Core i7-14700K", InterfaceId = 3, TDP = 253, Price = 381.02M },
            new CPU { Id = 23, Name = "Core i5-14600K", InterfaceId = 3, TDP = 125, Price = 305.99M },
            new CPU { Id = 24, Name = "Core i9-13900K", InterfaceId = 3, TDP = 360, Price = 458.18M },
            new CPU { Id = 25, Name = "Core i7-13700K", InterfaceId = 3, TDP = 253, Price = 309.98M },
            new CPU { Id = 26, Name = "Core i5-13600K", InterfaceId = 3, TDP = 125, Price = 259.96M },
            new CPU { Id = 27, Name = "Core i9-12900K", InterfaceId = 3, TDP = 241, Price = 299.99M },
            new CPU { Id = 28, Name = "Core i7-12700K", InterfaceId = 3, TDP = 190, Price = 229.99M },
            new CPU { Id = 29, Name = "Core i5-12600K", InterfaceId = 3, TDP = 125, Price = 175.19M }
        });

        // Seed data for GPUs
        modelBuilder.Entity<GPU>().HasData(new GPU[]
        {
            new GPU { Id = 1, Name = "RTX 4090", InterfaceId = 6, TDP = 450, Price = 1599.99M },
            new GPU { Id = 2, Name = "RTX 4080", InterfaceId = 6, TDP = 320, Price = 1199.99M },
            new GPU { Id = 3, Name = "RX 7900 XTX", InterfaceId = 6, TDP = 355, Price = 999.99M },
            new GPU { Id = 4, Name = "RX 7900 XT", InterfaceId = 6, TDP = 300, Price = 899.99M },
            new GPU { Id = 5, Name = "RTX 4070 Ti", InterfaceId = 6, TDP = 285, Price = 799.99M },
            new GPU { Id = 6, Name = "RX 6800 XT", InterfaceId = 6, TDP = 300, Price = 649.99M },
            new GPU { Id = 7, Name = "RTX 3060 Ti", InterfaceId = 6, TDP = 200, Price = 399.99M },
            new GPU { Id = 8, Name = "RX 6700 XT", InterfaceId = 6, TDP = 230, Price = 479.99M },
            new GPU { Id = 9, Name = "Intel Arc A770", InterfaceId = 6, TDP = 225, Price = 329.99M },
            new GPU { Id = 10, Name = "Intel Arc A750", InterfaceId = 6, TDP = 200, Price = 289.99M }
        });

        // Seed data for PSUs
        modelBuilder.Entity<PSU>().HasData(new PSU[]
        {
            new PSU { Id = 1, Name = "Corsair RM850x", Wattage = 850, Price = 129.99M },
            new PSU { Id = 2, Name = "EVGA SuperNOVA 1000 G5", Wattage = 1000, Price = 179.99M },
            new PSU { Id = 3, Name = "Seasonic Prime TX-750", Wattage = 750, Price = 159.99M },
            new PSU { Id = 4, Name = "Cooler Master V850", Wattage = 850, Price = 139.99M },
            new PSU { Id = 5, Name = "Corsair SF600", Wattage = 600, Price = 124.99M },
            new PSU { Id = 6, Name = "Be Quiet! Straight Power 11", Wattage = 750, Price = 149.99M },
            new PSU { Id = 7, Name = "Thermaltake Toughpower GF1", Wattage = 850, Price = 139.99M },
            new PSU { Id = 8, Name = "Corsair AX1000", Wattage = 1000, Price = 249.99M }
        });

        // Seed data for Coolers
        modelBuilder.Entity<Cooler>().HasData(new Cooler[]
        {
            new Cooler { Id = 1, Name = "Noctua NH-D15", TDP = 250, Price = 89.99M },
            new Cooler { Id = 2, Name = "Corsair H150i Elite", TDP = 280, Price = 159.99M },
            new Cooler { Id = 3, Name = "Cooler Master Hyper 212", TDP = 180, Price = 39.99M },
            new Cooler { Id = 4, Name = "NZXT Kraken Z63", TDP = 280, Price = 199.99M },
            new Cooler { Id = 5, Name = "Be Quiet! Dark Rock Pro 4", TDP = 250, Price = 89.99M },
            new Cooler { Id = 6, Name = "Arctic Liquid Freezer II", TDP = 300, Price = 129.99M }
        });

        // Seed data for Motherboards
        modelBuilder.Entity<Motherboard>().HasData(new Motherboard[]
        {
            new Motherboard { Id = 1, Name = "ASUS ROG Crosshair VIII X670", CPUInterfaceId = 1, GPUInterfaceId = 6, MemoryInterfaceId = 4, MemorySlots = 4, M2StorageSlots = 2, SataStorageSlots = 6, Price = 399.99M },
            new Motherboard { Id = 2, Name = "MSI MPG Z690", CPUInterfaceId = 3, GPUInterfaceId = 6, MemoryInterfaceId = 4, MemorySlots = 4, M2StorageSlots = 3, SataStorageSlots = 6, Price = 299.99M },
            new Motherboard { Id = 3, Name = "Gigabyte Aorus X570", CPUInterfaceId = 2, GPUInterfaceId = 6, MemoryInterfaceId = 5, MemorySlots = 4, M2StorageSlots = 2, SataStorageSlots = 6, Price = 259.99M },
            new Motherboard { Id = 4, Name = "ASRock Z690 Taichi", CPUInterfaceId = 3, GPUInterfaceId = 6, MemoryInterfaceId = 4, MemorySlots = 4, M2StorageSlots = 3, SataStorageSlots = 6, Price = 349.99M },
            new Motherboard { Id = 5, Name = "ASUS TUF Gaming B550", CPUInterfaceId = 2, GPUInterfaceId = 6, MemoryInterfaceId = 5, MemorySlots = 4, M2StorageSlots = 2, SataStorageSlots = 6, Price = 189.99M },
            new Motherboard { Id = 6, Name = "MSI MAG B550 Tomahawk", CPUInterfaceId = 2, GPUInterfaceId = 6, MemoryInterfaceId = 5, MemorySlots = 4, M2StorageSlots = 2, SataStorageSlots = 6, Price = 179.99M },
            new Motherboard { Id = 7, Name = "Gigabyte Z690 Aorus Master", CPUInterfaceId = 3, GPUInterfaceId = 6, MemoryInterfaceId = 4, MemorySlots = 4, M2StorageSlots = 3, SataStorageSlots = 6, Price = 469.99M },
            new Motherboard { Id = 8, Name = "ASRock B550 Steel Legend", CPUInterfaceId = 2, GPUInterfaceId = 6, MemoryInterfaceId = 5, MemorySlots = 4, M2StorageSlots = 2, SataStorageSlots = 6, Price = 159.99M },
            new Motherboard { Id = 9, Name = "ASUS Prime Z690-P", CPUInterfaceId = 3, GPUInterfaceId = 6, MemoryInterfaceId = 4, MemorySlots = 4, M2StorageSlots = 3, SataStorageSlots = 6, Price = 219.99M },
            new Motherboard { Id = 10, Name = "MSI MEG Z690 Unify", CPUInterfaceId = 3, GPUInterfaceId = 6, MemoryInterfaceId = 4, MemorySlots = 4, M2StorageSlots = 3, SataStorageSlots = 6, Price = 389.99M },
            new Motherboard { Id = 11, Name = "ASRock B650M Pro RS Wifi", CPUInterfaceId = 1, GPUInterfaceId = 6, MemoryInterfaceId = 4, MemorySlots = 4, M2StorageSlots = 3, SataStorageSlots = 4, Price = 389.99M }
        });

        // Seed data for Memories
        modelBuilder.Entity<Memory>().HasData(new Memory[]
        {
            new Memory { Id = 1, Name = "Corsair Vengeance DDR5", SizeGB = 32, InterfaceId = 4, Price = 179.99M },
            new Memory { Id = 2, Name = "Corsair Vengeance DDR5", SizeGB = 16, InterfaceId = 4, Price = 99.99M },
            new Memory { Id = 3, Name = "G.Skill Trident Z5 RGB", SizeGB = 32, InterfaceId = 4, Price = 199.99M },
            new Memory { Id = 4, Name = "G.Skill Trident Z5 RGB", SizeGB = 64, InterfaceId = 4, Price = 349.99M },
            new Memory { Id = 5, Name = "Kingston Fury Beast DDR5", SizeGB = 16, InterfaceId = 4, Price = 99.99M },
            new Memory { Id = 6, Name = "Kingston Fury Beast DDR5", SizeGB = 32, InterfaceId = 4, Price = 179.99M },
            new Memory { Id = 7, Name = "Corsair Dominator Platinum DDR5", SizeGB = 64, InterfaceId = 4, Price = 399.99M },
            new Memory { Id = 8, Name = "Corsair Dominator Platinum DDR5", SizeGB = 32, InterfaceId = 4, Price = 219.99M },
            new Memory { Id = 9, Name = "G.Skill Ripjaws V DDR5", SizeGB = 16, InterfaceId = 4, Price = 89.99M },
            new Memory { Id = 10, Name = "G.Skill Ripjaws V DDR5", SizeGB = 32, InterfaceId = 4, Price = 169.99M },
            new Memory { Id = 11, Name = "Kingston HyperX Predator DDR5", SizeGB = 32, InterfaceId = 4, Price = 179.99M },
            new Memory { Id = 12, Name = "Kingston HyperX Predator DDR5", SizeGB = 64, InterfaceId = 4, Price = 329.99M },
            new Memory { Id = 13, Name = "Crucial Ballistix DDR4", SizeGB = 16, InterfaceId = 5, Price = 44.99M },
            new Memory { Id = 14, Name = "Crucial Ballistix DDR4", SizeGB = 32, InterfaceId = 5, Price = 68.99M }
        });

        // Seed data for Storages
        modelBuilder.Entity<Storage>().HasData(new Storage[]
        {
            new Storage { Id = 1, Name = "Samsung 980 Pro", SizeGB = 1000, InterfaceId = 8, Price = 179.99M },
            new Storage { Id = 2, Name = "Samsung 980 Pro", SizeGB = 2000, InterfaceId = 8, Price = 349.99M },
            new Storage { Id = 3, Name = "WD Black SN850X", SizeGB = 2000, InterfaceId = 8, Price = 299.99M },
            new Storage { Id = 4, Name = "WD Black SN850X", SizeGB = 1000, InterfaceId = 8, Price = 169.99M },
            new Storage { Id = 5, Name = "Crucial MX500", SizeGB = 1000, InterfaceId = 7, Price = 99.99M },
            new Storage { Id = 6, Name = "Crucial MX500", SizeGB = 500, InterfaceId = 7, Price = 59.99M },
            new Storage { Id = 7, Name = "Seagate FireCuda 530", SizeGB = 2000, InterfaceId = 8, Price = 349.99M },
            new Storage { Id = 8, Name = "Seagate FireCuda 530", SizeGB = 1000, InterfaceId = 8, Price = 199.99M },
            new Storage { Id = 9, Name = "Samsung 970 Evo Plus", SizeGB = 500, InterfaceId = 8, Price = 89.99M },
            new Storage { Id = 10, Name = "Samsung 970 Evo Plus", SizeGB = 1000, InterfaceId = 8, Price = 159.99M },
            new Storage { Id = 11, Name = "WD Blue 3D NAND", SizeGB = 500, InterfaceId = 7, Price = 59.99M },
            new Storage { Id = 12, Name = "WD Blue 3D NAND", SizeGB = 1000, InterfaceId = 7, Price = 99.99M }
        });

        // Seed data for Builds
        modelBuilder.Entity<Build>().HasData(new Build[]
        {
            new Build { Id = 1, UserProfileId = 1, Name = "Ethan's Setup", Content = "After graduating, he finally had the time to put together a true gaming rig! He's been super excited to run games with high settings at high framerates!", CPUId = 4, CoolerId = 1, GPUId = 5, MotherboardId = 11, PSUId = 1, DateCreated = DateTime.Now.AddDays(-45) },
            new Build { Id = 2, UserProfileId = 2, Name = "Animation and Gaming Rig", Content = "Been learning how to animate for multiple years, and it's time to step up my game! Literally!", CPUId = 9, CoolerId = 3, GPUId = 9, MotherboardId = 6, PSUId = 5, DateCreated = DateTime.Now.AddDays(-45).AddHours(3) },
            new Build { Id = 3, UserProfileId = 3, Name = "BeatSaber Machine", Content = "I need all the frames I can get!! I'm still waiting for a 360Hz display on a headset", CPUId = 21, CoolerId = 6, GPUId = 1, MotherboardId = 9, PSUId = 8, DateCreated = DateTime.Now.AddDays(-44) },
            new Build { Id = 4, UserProfileId = 1, Name = "High-End Gaming PC", Content = "Been wanting to play games with ray tracing, and I thought now was the time", CPUId = 1, GPUId = 1, PSUId = 1, MotherboardId = 1, CoolerId = 1, DateCreated = DateTime.Now.AddDays(-40) },
            new Build { Id = 5, UserProfileId = 2, Name = "Mid-Range Gaming PC", Content = "Mostly just to play competitive games, I don't need anything fancy", CPUId = 9, GPUId = 3, PSUId = 2, MotherboardId = 5, CoolerId = 3, DateCreated = DateTime.Now.AddDays(-39) },
            new Build { Id = 6, UserProfileId = 3, Name = "Budget Gaming PC", Content = "Putting this together for my nephew, he mostly just plays Minecraft all day!", CPUId = 14, GPUId = 7, PSUId = 3, MotherboardId = 6, CoolerId = 5, DateCreated = DateTime.Now.AddDays(-35) },
            new Build { Id = 7, UserProfileId = 4, Name = "Content Creation PC", Content = "At this point my iPad was faster at editing my videos!! Seemed like it was time... my poor wallet", CPUId = 23, GPUId = 6, PSUId = 4, MotherboardId = 2, CoolerId = 2, DateCreated = DateTime.Now.AddDays(-29) },
            new Build { Id = 8, UserProfileId = 5, Name = "Office Workstation", Content = "I didn't really even have to pay for it! My job is reimbursing me next week", CPUId = 18, GPUId = 9, PSUId = 5, MotherboardId = 8, CoolerId = 6, DateCreated = DateTime.Now.AddDays(-29).AddHours(2) },
            new Build { Id = 9, UserProfileId = 6, Name = "Streaming PC", Content = "Maybe now I can finally stream at higher than 480p consistently! My one viewer will be so happy", CPUId = 4, GPUId = 2, PSUId = 6, MotherboardId = 11, CoolerId = 1, DateCreated = DateTime.Now.AddDays(-19) },
            new Build { Id = 10, UserProfileId = 1, Name = "HTPC", Content = "Putting this in my living room so that I can watch movies from my PLeX server, and stream co-op games from my main PC", CPUId = 7, GPUId = 8, PSUId = 7, MotherboardId = 5, CoolerId = 4, DateCreated = DateTime.Now.AddDays(-18) },
            new Build { Id = 11, UserProfileId = 2, Name = "Workstation PC", Content = "Hoping to be able to do lots of simulations on this bloody thing. I'm sitting around for hours with my PC unusable half of the day now", CPUId = 25, GPUId = 4, PSUId = 8, MotherboardId = 9, CoolerId = 5, DateCreated = DateTime.Now.AddDays(-4) },
            new Build { Id = 12, UserProfileId = 3, Name = "AI Development PC", Content = "My cousin is going nuts about AI, won't shut up about it! I'm hoping this will give him an outlet to see if he can do something with that energy", CPUId = 21, GPUId = 1, PSUId = 1, MotherboardId = 7, CoolerId = 2, DateCreated = DateTime.Now.AddDays(-1) },
            new Build { Id = 13, UserProfileId = 4, Name = "Graphic Design PC", Content = "Doesn't have to do too much! Illustrator and CSP are all I need~", CPUId = 26, GPUId = 5, PSUId = 3, MotherboardId = 4, CoolerId = 6, DateCreated = DateTime.Now.AddHours(-4) }
        });

        // Seed data for BuildMemories
        modelBuilder.Entity<BuildMemory>().HasData(new BuildMemory[]
        {
            new BuildMemory { BuildId = 1, MemoryId = 6, Quantity = 1 },
            new BuildMemory { BuildId = 2, MemoryId = 14, Quantity = 2 },
            new BuildMemory { BuildId = 3, MemoryId = 4, Quantity = 1 },
            new BuildMemory { BuildId = 4, MemoryId = 1, Quantity = 2 },
            new BuildMemory { BuildId = 5, MemoryId = 6, Quantity = 2 },
            new BuildMemory { BuildId = 6, MemoryId = 10, Quantity = 2 },
            new BuildMemory { BuildId = 7, MemoryId = 3, Quantity = 2 },
            new BuildMemory { BuildId = 8, MemoryId = 12, Quantity = 2 },
            new BuildMemory { BuildId = 9, MemoryId = 4, Quantity = 2 },
            new BuildMemory { BuildId = 10, MemoryId = 8, Quantity = 2 },
            new BuildMemory { BuildId = 11, MemoryId = 5, Quantity = 4 },
            new BuildMemory { BuildId = 12, MemoryId = 2, Quantity = 4 },
            new BuildMemory { BuildId = 13, MemoryId = 7, Quantity = 2 }

        });

        // Seed data for BuildStorages
        modelBuilder.Entity<BuildStorage>().HasData(new BuildStorage[]
        {
            new BuildStorage { BuildId = 1, StorageId = 3, Quantity = 1 },
            new BuildStorage { BuildId = 1, StorageId = 12, Quantity = 2 },
            new BuildStorage { BuildId = 2, StorageId = 3, Quantity = 2 },
            new BuildStorage { BuildId = 2, StorageId = 5, Quantity = 2 },
            new BuildStorage { BuildId = 3, StorageId = 2, Quantity = 1 },
            new BuildStorage { BuildId = 4, StorageId = 1, Quantity = 1 },
            new BuildStorage { BuildId = 4, StorageId = 2, Quantity = 2 },
            new BuildStorage { BuildId = 5, StorageId = 4, Quantity = 1 },
            new BuildStorage { BuildId = 6, StorageId = 8, Quantity = 1 },
            new BuildStorage { BuildId = 6, StorageId = 9, Quantity = 1 },
            new BuildStorage { BuildId = 7, StorageId = 3, Quantity = 2 },
            new BuildStorage { BuildId = 8, StorageId = 10, Quantity = 1 },
            new BuildStorage { BuildId = 9, StorageId = 7, Quantity = 2 },
            new BuildStorage { BuildId = 9, StorageId = 12, Quantity = 1 },
            new BuildStorage { BuildId = 10, StorageId = 6, Quantity = 2 },
            new BuildStorage { BuildId = 11, StorageId = 5, Quantity = 2 },
            new BuildStorage { BuildId = 12, StorageId = 4, Quantity = 2 },
            new BuildStorage { BuildId = 13, StorageId = 1, Quantity = 1 },
            new BuildStorage { BuildId = 13, StorageId = 7, Quantity = 1 }
        });

        // Seed data for Comments
        modelBuilder.Entity<Comment>().HasData(new Comment[]
        {
            new Comment { Id = 1, UserProfileId = 1, BuildId = 2, Content = "Good luck on your animations! I hope you find success", DateCreated = DateTime.Now.AddDays(-43) },
            new Comment { Id = 2, UserProfileId = 4, BuildId = 2, Content = "This should work out really for what you're working on!", DateCreated = DateTime.Now.AddDays(-43).AddHours(2) },
            new Comment { Id = 3, UserProfileId = 2, BuildId = 1, Content = "Perfect timing, there are so many good games coming out now!", DateCreated = DateTime.Now.AddDays(-41) },
            new Comment { Id = 4, UserProfileId = 2, BuildId = 3, Content = "I've really been meaning to play that game! Is it as much of a workout as I've heard?", DateCreated = DateTime.Now.AddDays(-41) },
            new Comment { Id = 5, UserProfileId = 5, BuildId = 1, Content = "I'm sure you're going to look so silly playing that haha", DateCreated = DateTime.Now.AddDays(-41) },
            new Comment { Id = 6, UserProfileId = 2, BuildId = 1, Content = "Great build, but have you considered a more powerful PSU?", DateCreated = new DateTime(2023, 6, 15) },
            new Comment { Id = 7, UserProfileId = 3, BuildId = 2, Content = "I think you can improve this by adding more RAM.", DateCreated = new DateTime(2023, 6, 16) },
            new Comment { Id = 8, UserProfileId = 4, BuildId = 3, Content = "Love the GPU choice!", DateCreated = new DateTime(2023, 6, 17) },
            new Comment { Id = 9, UserProfileId = 5, BuildId = 4, Content = "Looks good, but you might want a better cooler.", DateCreated = new DateTime(2023, 6, 18) },
            new Comment { Id = 10, UserProfileId = 6, BuildId = 5, Content = "Are you planning to overclock?", DateCreated = new DateTime(2023, 6, 19) },
            new Comment { Id = 11, UserProfileId = 3, BuildId = 1, Content = "How's the performance in gaming?", DateCreated = new DateTime(2023, 6, 20) },
            new Comment { Id = 12, UserProfileId = 4, BuildId = 2, Content = "Any issues with the motherboard?", DateCreated = new DateTime(2023, 6, 21) },
            new Comment { Id = 13, UserProfileId = 5, BuildId = 3, Content = "What's your total budget for this build?", DateCreated = new DateTime(2023, 6, 22) },
            new Comment { Id = 14, UserProfileId = 6, BuildId = 4, Content = "Consider adding more storage.", DateCreated = new DateTime(2023, 6, 23) },
            new Comment { Id = 15, UserProfileId = 2, BuildId = 5, Content = "Nice build! How's the cooling?", DateCreated = new DateTime(2023, 6, 24) },
            new Comment { Id = 16, UserProfileId = 5, BuildId = 1, Content = "Is this build for gaming or work?", DateCreated = new DateTime(2023, 6, 25) },
            new Comment { Id = 17, UserProfileId = 6, BuildId = 2, Content = "Are you planning to add a second GPU?", DateCreated = new DateTime(2023, 6, 26) },
            new Comment { Id = 18, UserProfileId = 2, BuildId = 3, Content = "Great choice of components!", DateCreated = new DateTime(2023, 6, 27) },
            new Comment { Id = 19, UserProfileId = 3, BuildId = 4, Content = "How's the noise level?", DateCreated = new DateTime(2023, 6, 28) },
            new Comment { Id = 20, UserProfileId = 4, BuildId = 5, Content = "Do you need all that power?", DateCreated = new DateTime(2023, 6, 29) }
        });

        // Seed data for Replies
        modelBuilder.Entity<Reply>().HasData(new Reply[]
        {
            new Reply { Id = 1, CommentId = 1, UserProfileId = 2, Content = "Thanks! Trying not to let imposter syndrome get to me", DateCreated = DateTime.Now.AddDays(-43).AddHours(1) },
            new Reply { Id = 2, CommentId = 2, UserProfileId = 2, Content = "Oh that's a relief, I was definitely a bit worried", DateCreated = DateTime.Now.AddDays(-43).AddHours(5) },
            new Reply { Id = 6, CommentId = 1, UserProfileId = 2, Content = "Thanks! I'll consider upgrading the RAM.", DateCreated = new DateTime(2023, 6, 16) },
            new Reply { Id = 7, CommentId = 1, UserProfileId = 3, Content = "You're welcome! Let us know how it goes.", DateCreated = new DateTime(2023, 6, 17) },
            new Reply { Id = 8, CommentId = 2, UserProfileId = 4, Content = "I appreciate the suggestion.", DateCreated = new DateTime(2023, 6, 18) },
            new Reply { Id = 9, CommentId = 3, UserProfileId = 5, Content = "Yes, I'm planning to overclock.", DateCreated = new DateTime(2023, 6, 19) },
            new Reply { Id = 10, CommentId = 3, UserProfileId = 6, Content = "Let us know your results!", DateCreated = new DateTime(2023, 6, 20) },
            new Reply { Id = 11, CommentId = 4, UserProfileId = 2, Content = "I'll keep that in mind. Thanks!", DateCreated = new DateTime(2023, 6, 21) },
            new Reply { Id = 12, CommentId = 5, UserProfileId = 3, Content = "It's primarily for gaming.", DateCreated = new DateTime(2023, 6, 22) },
            new Reply { Id = 13, CommentId = 6, UserProfileId = 4, Content = "The performance is excellent so far.", DateCreated = new DateTime(2023, 6, 23) },
            new Reply { Id = 14, CommentId = 7, UserProfileId = 5, Content = "No issues so far. It's been great.", DateCreated = new DateTime(2023, 6, 24) },
            new Reply { Id = 15, CommentId = 8, UserProfileId = 6, Content = "My budget is around $1500.", DateCreated = new DateTime(2023, 6, 25) },
            new Reply { Id = 16, CommentId = 9, UserProfileId = 2, Content = "I'm thinking about it. Any recommendations?", DateCreated = new DateTime(2023, 6, 26) },
            new Reply { Id = 17, CommentId = 10, UserProfileId = 3, Content = "The cooling is quite efficient.", DateCreated = new DateTime(2023, 6, 27) },
            new Reply { Id = 18, CommentId = 11, UserProfileId = 4, Content = "It's mainly for gaming purposes.", DateCreated = new DateTime(2023, 6, 28) },
            new Reply { Id = 19, CommentId = 12, UserProfileId = 5, Content = "No, I think one GPU is enough for now.", DateCreated = new DateTime(2023, 6, 29) },
            new Reply { Id = 20, CommentId = 13, UserProfileId = 6, Content = "Thank you! I'm happy with my choices.", DateCreated = new DateTime(2023, 6, 30) },
            new Reply { Id = 21, CommentId = 14, UserProfileId = 2, Content = "The noise level is minimal.", DateCreated = new DateTime(2023, 7, 1) },
            new Reply { Id = 22, CommentId = 15, UserProfileId = 3, Content = "Yes, I need it for intensive tasks.", DateCreated = new DateTime(2023, 7, 2) },
            new Reply { Id = 23, CommentId = 16, UserProfileId = 4, Content = "Good to know! Thanks.", DateCreated = new DateTime(2023, 7, 3) },
            new Reply { Id = 24, CommentId = 17, UserProfileId = 5, Content = "Not planning to add another one for now.", DateCreated = new DateTime(2023, 7, 4) },
            new Reply { Id = 25, CommentId = 18, UserProfileId = 6, Content = "I agree! The components are top-notch.", DateCreated = new DateTime(2023, 7, 5) },
            new Reply { Id = 26, CommentId = 1, UserProfileId = 2, Content = "I totally agree with your point!", DateCreated = DateTime.Now.AddDays(-3) },
            new Reply { Id = 27, CommentId = 1, UserProfileId = 3, Content = "Interesting perspective. Can you elaborate?", DateCreated = DateTime.Now.AddDays(-2) },
            new Reply { Id = 28, CommentId = 2, UserProfileId = 4, Content = "This was really helpful, thanks!", DateCreated = DateTime.Now.AddDays(-5) },
            new Reply { Id = 29, CommentId = 2, UserProfileId = 5, Content = "I had a different experience.", DateCreated = DateTime.Now.AddDays(-1) },
            new Reply { Id = 30, CommentId = 3, UserProfileId = 6, Content = "Can you provide more details?", DateCreated = DateTime.Now.AddDays(-4) },
            new Reply { Id = 31, CommentId = 3, UserProfileId = 2, Content = "That's a great point!", DateCreated = DateTime.Now.AddDays(-2) },
            new Reply { Id = 32, CommentId = 4, UserProfileId = 3, Content = "I disagree with your conclusion.", DateCreated = DateTime.Now.AddDays(-3) },
            new Reply { Id = 33, CommentId = 4, UserProfileId = 4, Content = "Thanks for sharing this!", DateCreated = DateTime.Now.AddDays(-2) },
            new Reply { Id = 34, CommentId = 5, UserProfileId = 5, Content = "Could you share your sources?", DateCreated = DateTime.Now.AddDays(-6) },
            new Reply { Id = 35, CommentId = 5, UserProfileId = 6, Content = "This is very insightful.", DateCreated = DateTime.Now.AddDays(-1) },
            new Reply { Id = 36, CommentId = 6, UserProfileId = 2, Content = "I appreciate your input.", DateCreated = DateTime.Now.AddDays(-5) },
            new Reply { Id = 37, CommentId = 6, UserProfileId = 3, Content = "Great analysis, thanks!", DateCreated = DateTime.Now.AddDays(-3) },
            new Reply { Id = 38, CommentId = 7, UserProfileId = 4, Content = "I learned a lot from this.", DateCreated = DateTime.Now.AddDays(-2) },
            new Reply { Id = 39, CommentId = 7, UserProfileId = 5, Content = "Could you clarify this point?", DateCreated = DateTime.Now.AddDays(-4) },
            new Reply { Id = 40, CommentId = 8, UserProfileId = 6, Content = "That's a valid concern.", DateCreated = DateTime.Now.AddDays(-1) },
            new Reply { Id = 41, CommentId = 8, UserProfileId = 2, Content = "Thanks for the heads up!", DateCreated = DateTime.Now.AddDays(-3) },
            new Reply { Id = 42, CommentId = 9, UserProfileId = 3, Content = "Can you explain this further?", DateCreated = DateTime.Now.AddDays(-2) },
            new Reply { Id = 43, CommentId = 9, UserProfileId = 4, Content = "I agree with your statement.", DateCreated = DateTime.Now.AddDays(-1) },
            new Reply { Id = 44, CommentId = 10, UserProfileId = 5, Content = "I had the same question.", DateCreated = DateTime.Now.AddDays(-6) },
            new Reply { Id = 45, CommentId = 10, UserProfileId = 6, Content = "This is really informative.", DateCreated = DateTime.Now.AddDays(-2) }
        });
    }
}