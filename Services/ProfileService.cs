using passnager_api;
using Microsoft.EntityFrameworkCore;

public class ProfileService : IProfileService
{
    private readonly DataContext _context;

    public ProfileService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProfileEntity>> GetAll()
    {
        return await _context.Profile.Where(p => p.IsActive).ToListAsync();
    }

    public async Task<ProfileEntity> GetById(int id)
    {
        return await _context.Profile.FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
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

    public async Task<ProfileEntity> UpdateById(ProfileEntity profile)
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
}

