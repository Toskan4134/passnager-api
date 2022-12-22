using passnager_api;

public interface IProfileService
{
    Task<List<ProfileEntity>> GetAll();
    Task<ProfileEntity> GetById(int id);
    Task<ProfileEntity> Create(ProfileEntity profile);
    Task<ProfileEntity> UpdateById(ProfileEntity profile);
}