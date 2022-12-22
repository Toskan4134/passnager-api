using passnager_api;

public interface IProfileService
{
    Task<IEnumerable<Profile>> GetAll();
    Task<Profile> GetById(int id);
    Task<Profile> Create(Profile profile);
    Task<Profile> UpdateById(Profile profile);
}