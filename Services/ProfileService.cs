using passnager_api;
using Microsoft.EntityFrameworkCore;
using NLog;
public class ProfileService : IProfileService
{
    private readonly DataContext _context;
    private readonly NLog.ILogger _logger;

    public ProfileService(DataContext context, NLog.ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ProfileEntity>> GetAll()
    {
        return await _context.Profile.Where(p => p.IsActive).OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<ProfileEntity> GetProfileById(int id)
    {
        return await _context.Profile.FirstOrDefaultAsync(p => p.IsActive && p.Id == id);
    }

    public async Task<Boolean> CheckLogin(ProfileEntity profile)
    {
        var existingProfile = await _context.Profile.FirstOrDefaultAsync(p => p.Id == profile.Id && p.Password == profile.Password && p.IsActive);
        if (existingProfile != null)
        {
            return true;
        }
        return false;
    }

    public async Task<ProfileEntity> Create(ProfileEntity profile)
    {

        var newprofile = new ProfileEntity
        {
            IsActive = true,
            Name = profile.Name,
            Icon = profile.Icon,
            Password = profile.Password
        };

        // Guarda el nuevo perfil en la base de datos
        await _context.Profile.AddAsync(newprofile);
        await _context.SaveChangesAsync();

        return profile;
    }

    public async Task<ProfileEntity> Update(ProfileEntity profile)
    {
        if (profile.Id == 0)
        {
            return null;
        }
        var existingProfile = await _context.Profile.FirstOrDefaultAsync(p => p.Id == profile.Id && p.IsActive);
        if (existingProfile == null)
        {
            return null;
        }

        if (profile.Name != null)
        {
            existingProfile.Name = profile.Name;
        }
        if (profile.Password != null)
        {
            existingProfile.Password = profile.Password;
        }
        if (profile.Icon != null)
        {
            existingProfile.Icon = profile.Icon;
        }
        _context.Profile.Update(existingProfile);
        await _context.SaveChangesAsync();
        return existingProfile;
    }


    public async Task<ProfileEntity> DeleteById(int id)
    {
        if (id == 0)
        {
            return null;
        }
        var existingProfile = await _context.Profile.FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        if (existingProfile == null)
        {
            return null;
        }

        existingProfile.IsActive = false;
        _context.Profile.Update(existingProfile);
        await _context.SaveChangesAsync();
        return existingProfile;
    }
}

