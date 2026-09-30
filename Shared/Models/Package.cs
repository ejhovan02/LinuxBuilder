namespace LinuxBuilder.Shared.Models;

public class Package
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public string PackageName { get; set; } = "";

    //public PackageSource Source { get; set; }
}