using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models;
using Proyecto_lenguajes.Models.Services;
using System.Diagnostics;

namespace Proyecto_lenguajes.Controllers
{
    public class BreakingNewController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        BreakingNewServices breakingNewServices;

        public BreakingNewController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            breakingNewServices = new BreakingNewServices(_configuration);
        }

        public IActionResult Get(string idNot)
        {
            try
            {
                var result = breakingNewServices.Get(idNot);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Error();
                }
            } catch (SqlException)
            {
                throw;
            }
        }

        public IActionResult GetMaxId()
        {
            try
            {
                var result = breakingNewServices.GetMaxId();
                if (result != null)
                {
                    return Ok(result);
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
