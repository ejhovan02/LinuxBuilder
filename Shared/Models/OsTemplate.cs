using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinuxBuilder.Shared.Models;

public class OsTemplate
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("User")]
    public string UserId {get; set;}

    // Community / metadata
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    // Linux foundation
    public string BaseDistro { get; set; } = "";
    public string BaseVersion { get; set; } = "";
    public string Architecture { get; set; } = "x86_64";

    // Desktop
    public string DesktopEnvironment { get; set; } = "";
    public string Theme { get; set; } = "";

    // Software
    public List<Package> Packages { get; set; } = [];

    // System configuration
    public string Locale { get; set; } = "en_US.UTF-8";
    public string TimeZone { get; set; } = "America/New_York";
    public string KeyboardLayout { get; set; } = "us";

    // Installation
    public string PartitionPreset { get; set; } = "";

    // Template information
    public int TemplateVersion { get; set; } = 1;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
