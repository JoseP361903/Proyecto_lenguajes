using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models;
using Proyecto_lenguajes.Models.Entities;
using Proyecto_lenguajes.Models.Services;

namespace Proyecto_lenguajes.Controllers
{
    public class CommentNewController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        CommentNewServices commentNewServices;

        public CommentNewController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            commentNewServices = new CommentNewServices(_configuration);
        }

        public IActionResult Post([FromBody] CommentNew commentNew)
        {
            try
            {
                return Ok(commentNewServices.Post(commentNew));
            }
            catch (SqlException e)
            {
                //TODO Consider how to handle errors
                ViewBag.Message = e.Message;
                return View(e.ToString());
            }
        }

        public IActionResult Get(string id)
        {
            try
            {
                return Ok(commentNewServices.Get(id));
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
