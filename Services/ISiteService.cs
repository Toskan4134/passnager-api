using passnager_api;
public interface ISiteService
{
    Task<List<SiteEntity>> GetAllByCategoryId(int id);
    Task<List<SiteEntity>> GetByFilter(int id, string filter, string value);
    Task<SiteEntity> Create(SiteEntity site);
    Task<SiteEntity> Update(SiteEntity site);
    Task<SiteEntity> DeleteById(int id);
}