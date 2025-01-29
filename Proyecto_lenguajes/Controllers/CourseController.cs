using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models;
using Proyecto_lenguajes.Models.Services;
using System.Diagnostics;

namespace Proyecto_lenguajes.Controllers
{
    public class CourseController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        CourseServices courseServices;

        public CourseController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            courseServices = new CourseServices(_configuration);
        }

        public IActionResult GetByCycle(int cycle)
        {
            try
            {
                var getCourses = courseServices.Get(cycle);
                if (getCourses != null)
                {
                    return Ok(getCourses);
                }
                else
                {
                    return Error();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public IActionResult GetByAcronym(string acronym)
        {
            try
            {
                var getAcronymCourse = courseServices.Get(acronym);
                if (getAcronymCourse != null)
                {
                    return Ok(getAcronymCourse);
                }
                else
                {
                    return Error();
                }
            }
            catch (SqlException)
            {
                throw;
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
