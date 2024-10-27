namespace CitMovie.Api;

[ApiController]
[Route("api/languages")]

public class LanguageController : ControllerBase
{
    private readonly LanguageService _languageService;

    public LanguageController(LanguageService languageService)
    {
        _languageService = languageService;
    }

    [HttpGet("/")]
    public async Task<IActionResult> GetLanguages()
    {
        
        var languages = await _languageService.GetLanguagesAsync();
        
        return Ok(languages);
    }    
}