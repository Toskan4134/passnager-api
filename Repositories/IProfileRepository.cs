using passnager_api;

public interface IProfileRepository
{
    Task<IEnumerable<Profile>> GetAll();
    Task<Profile> GetById(int id);
    Task<Profile> Create(Profile profile);
    Task<Profile> UpdateById(int id, Profile profile);
}