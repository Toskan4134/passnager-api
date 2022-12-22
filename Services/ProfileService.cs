using passnager_api;
using Microsoft.EntityFrameworkCore;

public class ProfileService : IProfileService
{
    private readonly DataContext _context;

    public ProfileService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Profile>> GetAll()
    {
        return await _context.Profile.ToListAsync();
    }

    public async Task<Profile> GetById(int id)
    {
        return await _context.Profile.FindAsync(id);
    }

    public async Task<Profile> Create(Profile profile)
    {

        var newprofile = new Profile
        {
            Name = profile.Name,
            Icon = profile.Icon,
            Password = profile.Password
        };

        // Guarda el nuevo perfil en la base de datos
        await _context.Profile.AddAsync(newprofile);
        await _context.SaveChangesAsync();

        return profile;
    }

    public async Task<Profile> UpdateById(Profile profile)
    {
        if (profile.Id == 0)
        {
            return null;
        }
        var existingProfile = await _context.Profile.FindAsync(profile.Id);
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

