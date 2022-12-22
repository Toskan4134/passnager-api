using passnager_api;

public interface ISiteService
{
    Task<IEnumerable<SiteEntity>> GetAllByCategoryId(int id);
    Task<IEnumerable<SiteEntity>> GetByFilter(int id, string filter, string value);
    Task<SiteEntity> Create(SiteEntity site);
    Task<SiteEntity> UpdateById(SiteEntity site);
}