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
    public async Task<ActionResult<List<SiteEntity>>> GetAllByCategoryId(int id)
    {
        var sites = await _siteService.GetAllByCategoryId(id);
        return Ok(sites);
    }

    // GET /site/{id}/filter?value=xxx
    [HttpGet("{id}/{filter}")]
    public async Task<ActionResult<List<SiteEntity>>> GetByFilter(int id, string filter, [FromQuery(Name = "value")] string value)
    {
        var sites = await _siteService.GetByFilter(id, filter, value);
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
}