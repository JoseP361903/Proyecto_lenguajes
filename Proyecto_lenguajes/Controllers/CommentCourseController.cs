using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models.Entities;
using Proyecto_lenguajes.Models.Services;
using Proyecto_lenguajes.Models;

namespace Proyecto_lenguajes.Controllers
{
    public class CommentCourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        CommentCourseServices commentCourseServices;

        public CommentCourseController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            commentCourseServices = new CommentCourseServices(_configuration);
        }

        public IActionResult Post([FromBody] CommentCourse commentCourse)
        {
            try
            {
                return Ok(commentCourseServices.Post(commentCourse));
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
                return Ok(commentCourseServices.Get(id));
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
