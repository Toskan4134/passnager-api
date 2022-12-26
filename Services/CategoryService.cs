using passnager_api;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryEntity>> GetAllByProfileId(int id)
    {
        return await _context.Category.Where(c => c.ProfileId == id && c.IsActive).OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<CategoryEntity> Create(CategoryEntity category)
    {

        var newCategory = new CategoryEntity
        {
            IsActive = true,
            Name = category.Name,
            Icon = category.Icon,
            ProfileId = category.ProfileId

        };

        // Guarda la nueva categoría en la base de datos
        await _context.Category.AddAsync(newCategory);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<CategoryEntity> Update(CategoryEntity category)
    {
        if (category.Id == 0)
        {
            return null;
        }
        var existingCategory = await _context.Category.FirstOrDefaultAsync(c => c.Id == category.Id && c.IsActive);
        if (existingCategory == null)
        {
            return null;
        }

        if (category.Name != null)
        {
            existingCategory.Name = category.Name;
        }

        if (category.Icon != null)
        {
            existingCategory.Icon = category.Icon;
        }
        _context.Category.Update(existingCategory);
        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<CategoryEntity> DeleteById(int id)
    {
        if (id == 0)
        {
            return null;
        }
        var existingCategory = await _context.Category.FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        if (existingCategory == null)
        {
            return null;
        }

        existingCategory.IsActive = false;
        _context.Category.Update(existingCategory);
        await _context.SaveChangesAsync();
        return existingCategory;
    }
}
