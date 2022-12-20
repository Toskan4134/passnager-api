using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using passnager_api;

[Route("[controller]")]
public class ProfileController : ControllerBase
{
    // Aquí debes inyectar una dependencia que te permita acceder a la base de datos y modificar los perfiles
    private readonly IProfileRepository _profileRepository;
    private readonly DataContext _context;

    public ProfileController(IProfileRepository profileRepository, DataContext context)
    {
        _profileRepository = profileRepository;
        _context = context;
    }

    // GET /profiles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
    {
        // Llama al método que recupera todos los perfiles de la base de datos
        var profiles = await _profileRepository.GetAll();
        return Ok(profiles);
    }

    // GET /profiles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> GetProfileById(int id)
    {
        // Llama al método que recupera un perfil por su ID
        var profile = await _profileRepository.GetById(id);
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
        var newProfile = await _profileRepository.Create(profile);

        // Devuelve el nuevo perfil creado
        return Ok(newProfile);
    }

    // PUT /profiles/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> EditProfileById(int id, Profile profile)
    {
        // Llama al método que actualiza un perfil existente en la base de datos
        var profileEdited = await _profileRepository.UpdateById(id, profile);
        if (profileEdited.Equals(null))
        {
            return NotFound();
        }

        return Ok(profileEdited);
    }
}