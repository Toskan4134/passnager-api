using Microsoft.AspNetCore.Mvc;
using passnager_api;

[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly DataContext _context;

    public CategoryController(ICategoryService categoryService, DataContext context)
    {
        _categoryService = categoryService;
        _context = context;
    }

    // GET /category/GetByProfile/{profileId}
    [HttpGet("GetByProfile/{profileId}")]
    public async Task<ActionResult<CategoryEntity>> GetCategoriesByProfileId(int profileId)
    {
        var category = await _categoryService.GetAllByProfileId(profileId);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
    // GET /category/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryEntity>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    // POST /category
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryEntity category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Logger.addLog($"Categoría Creada: {category.Name}");
        var newCategory = await _categoryService.Create(category);

        return Ok(newCategory);
    }

    // PUT /category
    [HttpPut]
    public async Task<ActionResult> EditCategoryById([FromBody] CategoryEntity category)
    {
        var categoryEdited = await _categoryService.Update(category);
        if (categoryEdited == null)
        {
            return NotFound();
        }
        Logger.addLog($"Categoría Editada: {categoryEdited.Name} ({categoryEdited.Id})");

        return Ok(categoryEdited);
    }


    // DELETE /category/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategoryById(int id)
    {
        var categoryDeleted = await _categoryService.DeleteById(id);
        if (categoryDeleted == null)
        {
            Logger.addLog($"Categoría no encontrada al intentar borrarla: ID: {id}", "warn");
            return NotFound();
        }
        Logger.addLog($"Categoría Eliminada: {categoryDeleted.Name} ({id})");

        return Ok(categoryDeleted);
    }
}