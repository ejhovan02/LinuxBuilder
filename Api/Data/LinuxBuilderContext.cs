using Microsoft.EntityFrameworkCore;
using LinuxBuilder.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ApplicationUser : IdentityUser
{
    public List<OsTemplate> Templates { get; set; } = [];
}
public class LinuxBuilderContext : IdentityDbContext<ApplicationUser>
{
    public LinuxBuilderContext(DbContextOptions<LinuxBuilderContext> options) : base(options)
    {
        
    }
    public DbSet<Package> Packages { get; set; }
    public DbSet<OsTemplate> OsTemplates { get; set; }
}