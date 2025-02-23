using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;
using Proyecto_lenguajes.Models.Services;
using Proyecto_lenguajes.Models;

namespace Proyecto_lenguajes.Controllers
{
    public class PrivateConsultationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        PrivateConsultationServices privateConsultationServices;

        public PrivateConsultationController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            privateConsultationServices = new PrivateConsultationServices(_configuration);
        }

        public IActionResult Post([FromBody] PrivateConsultation privateConsultation)
        {
            try
            {
                return Ok(privateConsultationServices.Post(privateConsultation));
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
                return Ok(privateConsultationServices.GetPrivateByStudent(id));
            } catch (SqlException e)
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
                return Ok(privateConsultationServices.GetPrivateById(id));
            } catch (SqlException e)
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
