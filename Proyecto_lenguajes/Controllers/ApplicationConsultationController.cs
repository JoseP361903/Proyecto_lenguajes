using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models;
using Proyecto_lenguajes.Models.Entities;
using Proyecto_lenguajes.Models.Services;

namespace Proyecto_lenguajes.Controllers
{
    public class ApplicationConsultationController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        ApplicationConsultationServices applicationConsultationServices;

        public ApplicationConsultationController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            applicationConsultationServices = new ApplicationConsultationServices(_configuration);
        }

        public IActionResult Post([FromBody] ApplicationConsultation applicationConsultation)
        {
            try
            {
                return Ok(applicationConsultationServices.Post(applicationConsultation));
            }
            catch (SqlException e)
            {
                //TODO Consider how to handle errors
                ViewBag.Message = e.Message;
                return View(e.ToString());
            }
        }

        public IActionResult GetByStudent(string id)
        {
            try
            {
                return Ok(applicationConsultationServices.GetByStudent(id));
            }
            catch (SqlException e)
            {
                //TODO Consider how to handle errors
                ViewBag.Message = e.Message;
                return View(e.ToString());
            }
        }

        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(applicationConsultationServices.GetById(id));
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
