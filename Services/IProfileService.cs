using passnager_api;

public interface IProfileService
{
    Task<List<ProfileEntity>> GetAll();
    Task<ProfileEntity> GetProfileById(int id);
    Task<Boolean> CheckLogin(ProfileEntity profile);
    Task<ProfileEntity> Create(ProfileEntity profile);
    Task<ProfileEntity> Update(ProfileEntity profile);
}