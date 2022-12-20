using passnager_api;
using Microsoft.EntityFrameworkCore;

public class ProfileRepository : IProfileRepository
{
    private readonly DataContext _context;

    public ProfileRepository(DataContext context)
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
        _context.Profile.Add(newprofile);
        await _context.SaveChangesAsync();

        return profile;
    }

    public async Task<Profile> UpdateById(int id, Profile profile)
    {
        var existingProfile = await _context.Profile.FindAsync(id);
        if (existingProfile == null)
        {
            return null;
        }

        existingProfile.Name = profile.Name;
        existingProfile.Icon = profile.Icon;
        existingProfile.Password = profile.Password;
        _context.Profile.Update(existingProfile);
        await _context.SaveChangesAsync();
        return existingProfile;
    }
}

