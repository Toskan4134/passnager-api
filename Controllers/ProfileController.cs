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
    public async Task<ActionResult<List<ProfileEntity>>> GetProfiles()
    {
        // Llama al método que recupera todos los perfiles de la base de datos
        var profiles = await _profileService.GetAll();
        return Ok(profiles);
    }

    // GET /profiles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileEntity>> GetProfileById(int id)
    {
        // Llama al método que recupera todos los perfiles de la base de datos
        var profiles = await _profileService.GetProfileById(id);
        return Ok(profiles);
    }



    // Post /profiles/CheckLogin
    [HttpPost("CheckLogin")]
    public async Task<ActionResult<Boolean>> CheckLogin([FromBody] ProfileEntity profile)
    {
        // Llama al método que comprueba su ID y Contraseña
        var checkLogin = await _profileService.CheckLogin(profile);
        if (profile == null)
        {
            return NotFound();
        }
        if (checkLogin)
            Logger.addLog($"Sesión Iniciada Correctamente: ID {profile.Id}");
        else
            Logger.addLog($"Sesión Inválida: ID {profile.Id}", "warn");


        return Ok(checkLogin);
    }

    // POST /profiles
    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] ProfileEntity profile)
    {
        // Valida los datos del nuevo perfil
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Crea un nuevo objeto de perfil a partir de los datos del nuevo perfil
        var newProfile = await _profileService.Create(profile);
        Logger.addLog($"Perfil Creado: {newProfile.Name}");
        // Devuelve el nuevo perfil creado
        return Ok(newProfile);
    }

    // PUT /profiles
    [HttpPut]
    public async Task<ActionResult> EditProfile([FromBody] ProfileEntity profile)
    {
        // Llama al método que actualiza un perfil existente en la base de datos
        var profileEdited = await _profileService.Update(profile);
        if (profileEdited == null)
        {
            return NotFound();
        }
        Logger.addLog($"Perfil Editado: {profileEdited.Name} ({profileEdited.Id})");

        return Ok(profileEdited);
    }

    // DELETE /profiles/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProfileById(int id)
    {
        // Llama al método que elimina un perfil activo en la base de datos
        var profileDeleted = await _profileService.DeleteById(id);
        if (profileDeleted == null)
        {
            Logger.addLog($"Perfíl no encontrado al intentar borrarlo: ID: {id}", "warn");
            return NotFound();
        }
        Logger.addLog($"Perfíl Eliminado: {profileDeleted.Name} ({id})");
        return Ok(profileDeleted);
    }
}