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

    // GET /category/{id}
    [HttpGet("{profileId}")]
    public async Task<ActionResult<CategoryEntity>> GetCategoriesByProfileId(int profileId)
    {
        var category = await _categoryService.GetAllByProfileId(profileId);
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

        return Ok(categoryEdited);
    }


    // DELETE /category/{categoryId}
    [HttpDelete("{categoryId}")]
    public async Task<ActionResult> DeleteCategoryById(int categoryId)
    {
        var categoryDeleted = await _categoryService.DeleteById(categoryId);
        if (categoryDeleted == null)
        {
            return NotFound();
        }

        return Ok(categoryDeleted);
    }
}