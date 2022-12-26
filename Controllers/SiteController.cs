using Microsoft.AspNetCore.Mvc;
using passnager_api;

[Route("[controller]")]
public class SiteController : ControllerBase
{
    private readonly ISiteService _siteService;
    private readonly DataContext _context;

    public SiteController(ISiteService siteService, DataContext context)
    {
        _siteService = siteService;
        _context = context;
    }

    // GET /site/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SiteEntity>> GetSiteById(int id)
    {
        var site = await _siteService.GetSiteById(id);
        return Ok(site);
    }

    // GET /site/GetByCategory/{categoryId}
    [HttpGet("GetByCategory/{categoryId}")]
    public async Task<ActionResult<List<SiteEntity>>> GetAllByCategoryId(int categoryId)
    {
        var sites = await _siteService.GetAllByCategoryId(categoryId);
        return Ok(sites);
    }

    // GET /site/GetByCategory/{categoryId}/filter?value=xxx
    [HttpGet("GetByCategory/{categoryId}/{filter}")]
    public async Task<ActionResult<List<SiteEntity>>> GetByFilter(int categorId, string filter, [FromQuery(Name = "value")] string value)
    {
        var sites = await _siteService.GetByFilter(categorId, filter, value);
        return Ok(sites);
    }

    // POST /site
    [HttpPost]
    public async Task<IActionResult> CreateSite([FromBody] SiteEntity site)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newSite = await _siteService.Create(site);

        return Ok(newSite);
    }

    // PUT /site
    [HttpPut]
    public async Task<ActionResult> EditSite([FromBody] SiteEntity site)
    {
        var profileEdited = await _siteService.Update(site);
        if (profileEdited == null)
        {
            return NotFound();
        }

        return Ok(profileEdited);
    }

    // DELETE /site/{siteId}
    [HttpDelete("{siteId}")]
    public async Task<ActionResult> DeleteSiteById(int siteId)
    {
        // Llama al método que elimina un perfil activo en la base de datos
        var siteDeleted = await _siteService.DeleteById(siteId);
        if (siteDeleted == null)
        {
            return NotFound();
        }

        return Ok(siteDeleted);
    }
}