using Microsoft.AspNetCore.Mvc;
using Proyecto_lenguajes.Models.Entities;
using Proyecto_lenguajes.Models.Services;
using Proyecto_lenguajes.Models;
using System.Diagnostics;

public class BreakingNewController : Controller
{
    private readonly ILogger<BreakingNewController> _logger;
    private readonly IConfiguration _configuration;
    private readonly BreakingNewServices breakingNewServices;

    public BreakingNewController(ILogger<BreakingNewController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        breakingNewServices = new BreakingNewServices(_configuration);
    }

    [HttpGet]
    public IEnumerable<BreakingNew> Get()
    {
        IEnumerable<BreakingNew> news = null;

        try
        {
            news = breakingNewServices.GetAllNews();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las noticias");
            ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}");
            news = Enumerable.Empty<BreakingNew>();
        }

        return news;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}