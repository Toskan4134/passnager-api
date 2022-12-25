using passnager_api;

public interface ICategoryService
{
    Task<List<CategoryEntity>> GetAllByProfileId(int id);
    Task<CategoryEntity> Create(CategoryEntity category);
    Task<CategoryEntity> Update(CategoryEntity category);
    Task<CategoryEntity> DeleteById(int id);
}