using passnager_api;

public interface IProfileService
{
    Task<List<ProfileEntity>> GetAll();
    Task<Boolean> CheckLogin(ProfileEntity profile);
    Task<ProfileEntity> Create(ProfileEntity profile);
    Task<ProfileEntity> UpdateById(ProfileEntity profile);
}