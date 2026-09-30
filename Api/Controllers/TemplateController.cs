using Microsoft.AspNetCore.Mvc;
using LinuxBuilder.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TemplateController : ControllerBase
{
    private readonly LinuxBuilderContext _context;

    private readonly IMemoryCache _cache;

    public TemplateController(LinuxBuilderContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet("packages")]
    public async Task<IActionResult> GetPackages()
    {
        if (!_cache.TryGetValue("packages", out List<Package> packages))
        {
            packages = await _context.Packages
            .AsNoTracking()
            .ToListAsync();
            
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            
            _cache.Set("packages", packages, cacheEntryOptions);
        }
        return Ok(packages);
    }

    [HttpGet("os-templates")]
    public async Task<IActionResult> GetOsTemplates()
    {
        if (!_cache.TryGetValue("osTemplates", out List<OsTemplate> osTemplates))
        {
            osTemplates = await _context.OsTemplates
            .AsNoTracking()
            .ToListAsync();
            
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            
            _cache.Set("osTemplates", osTemplates, cacheEntryOptions);
        }
        return Ok(osTemplates);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOsTemplate(OsTemplate osTemplate)
    {
        _context.OsTemplates.Add(osTemplate);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOsTemplates), new { id = osTemplate.Id }, osTemplate);
    }
}