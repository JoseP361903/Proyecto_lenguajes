using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models;
using Proyecto_lenguajes.Models.Services;

namespace Proyecto_lenguajes.Controllers
{
    public class ProfessorController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        ProfessorServices professorServices;

        public ProfessorController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            professorServices = new ProfessorServices(_configuration);
        }

        public IActionResult Get()
        {
            try
            {
                return Ok(professorServices.Get());
            }
            catch (SqlException e)
            {
                //TODO Consider how to handle errors
                ViewBag.Message = e.Message;
                return View(e.ToString());
            }
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
}
