using Microsoft.AspNetCore.Mvc;
using passnager_api;

[Route("[controller]")]
public class ProfileController : ControllerBase
{
    // Aquí debes inyectar una dependencia que te permita acceder a la base de datos y modificar los perfiles
    private readonly IProfileService _profileService;
    private readonly DataContext _context;

    public ProfileController(IProfileService profileService, DataContext context)
    {
        _profileService = profileService;
        _context = context;
    }

    // GET /profiles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
    {
        // Llama al método que recupera todos los perfiles de la base de datos
        var profiles = await _profileService.GetAll();
        return Ok(profiles);
    }

    // GET /profiles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> GetProfileById(int id)
    {
        // Llama al método que recupera un perfil por su ID
        var profile = await _profileService.GetById(id);
        if (profile == null)
        {
            return NotFound();
        }

        return Ok(profile);
    }

    // POST /profiles
    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] Profile profile)
    {
        // Valida los datos del nuevo perfil
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Crea un nuevo objeto de perfil a partir de los datos del nuevo perfil
        var newProfile = await _profileService.Create(profile);

        // Devuelve el nuevo perfil creado
        return Ok(newProfile);
    }

    // PUT /profiles/{id}
    [HttpPut]
    public async Task<ActionResult> EditProfileById([FromBody] Profile profile)
    {
        // Llama al método que actualiza un perfil existente en la base de datos
        var profileEdited = await _profileService.UpdateById(profile);
        if (profileEdited == null)
        {
            return NotFound();
        }

        return Ok(profileEdited);
    }
}