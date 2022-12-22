using passnager_api;
using Microsoft.EntityFrameworkCore;

public class SiteService : ISiteService
{
    private readonly DataContext _context;

    public SiteService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<SiteEntity>> GetAllByCategoryId(int id)
    {
        return await _context.Site.Where(s => s.CategoryId == id && s.IsActive).ToListAsync();
    }

    public async Task<List<SiteEntity>> GetByFilter(int id, string filter, string value)
    {
        List<SiteEntity> filtered = null;
        value = value.ToLower();
        switch (filter)
        {
            case "all":
                filtered = await _context.Site.Where(s => s.CategoryId == id && (s.Site.ToLower().Contains(value) || s.Url.ToLower().Contains(value) || s.User.ToLower().Contains(value) || s.Date.ToString().ToLower().Contains(value) || s.Description.ToLower().Contains(value))).ToListAsync();
                break;
            case "site":
                filtered = await _context.Site.Where(s => s.CategoryId == id && s.Site.ToLower().Contains(value)).ToListAsync();
                break;
            case "url":
                filtered = await _context.Site.Where(s => s.CategoryId == id && s.Url.ToLower().Contains(value)).ToListAsync();
                break;
            case "user":
                filtered = await _context.Site.Where(s => s.CategoryId == id && s.User.ToLower().Contains(value)).ToListAsync();
                break;
            case "date":
                filtered = await _context.Site.Where(s => s.CategoryId == id && s.Date.ToString().ToLower().Contains(value)).ToListAsync();
                break;
            case "description":
                filtered = await _context.Site.Where(s => s.CategoryId == id && s.Description.ToLower().Contains(value)).ToListAsync();
                break;
        }
        return filtered ?? null;
    }


    public async Task<SiteEntity> Create(SiteEntity site)
    {

        var newSite = new SiteEntity
        {
            CategoryId = site.CategoryId,
            Date = DateTime.UtcNow,
            Description = site.Description,
            IsActive = true,
            Site = site.Site,
            Url = site.Url,
            User = site.User,
            Password = site.Password
        };

        await _context.Site.AddAsync(newSite);
        await _context.SaveChangesAsync();

        return site;
    }

    public async Task<SiteEntity> Update(SiteEntity site)
    {
        if (site.Id == 0)
        {
            return null;
        }
        var existingSite = await _context.Site.FirstOrDefaultAsync(s => s.Id == site.Id && s.IsActive);
        if (existingSite == null)
        {
            return null;
        }

        if (site.Site != null)
        {
            existingSite.Site = site.Site;
        }
        if (site.Password != null)
        {
            existingSite.Password = site.Password;
        }
        if (site.CategoryId != 0)
        {
            existingSite.CategoryId = site.CategoryId;
        }
        if (site.Description != null)
        {
            existingSite.Description = site.Description;
        }
        if (site.Url != null)
        {
            existingSite.Url = site.Url;
        }
        if (site.User != null)
        {
            existingSite.User = site.User;
        };
        _context.Site.Update(existingSite);
        await _context.SaveChangesAsync();
        return existingSite;
    }
}

