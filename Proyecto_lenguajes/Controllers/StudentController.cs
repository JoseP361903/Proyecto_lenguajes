using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto_lenguajes.Models;
using Proyecto_lenguajes.Models.Entities;
using Proyecto_lenguajes.Models.Services;

namespace Proyecto_lenguajes.Controllers
{
    public class StudentController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        StudentServices studentServices;

        public StudentController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            studentServices = new StudentServices(_configuration);
        }

        public IActionResult Get(string id)
        {
            try
            {
                if(studentServices.ValidateID(id) != null)
                {
                    return Ok(studentServices.Get(id));
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

        public IActionResult Authenticate([FromBody] Student student)
        {
            var result = studentServices.Authenticate(student);
            try
            {
                switch (result)
                {
                    case 1:
                        return Ok(student); // Autenticación exitosa
                    case -1:
                        return Unauthorized("Incorrect password."); // Contraseña incorrecta
                    case -2:
                        return NotFound("User does not exist."); // Usuario no existe
                    case -3:
                        return Forbid("User is not active."); // Usuario no está activo
                    default:
                        return StatusCode(500, "Unknown error."); // Error desconocido
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public IActionResult Post([FromBody] Student student)
        {
            try
            { 

                if (studentServices.ValidateID(student.Id).Id == null)
                {
                    return Ok(studentServices.Post(student));
                }
                else
                {
                    //TODO Consider how to handle errors
                    return Error();
                }
            }
            catch (SqlException e)
            {
                //TODO Consider how to handle errors
                ViewBag.Message = e.Message;
                return View(e.ToString());
            }
        }
        //
        public IActionResult Validate(string Id)
        {
            try
            {
                Student student = studentServices.ValidateID(Id);
                if (student.Id != null)
                {
                    return Ok();
                }
                else
                {
                    //TODO Consider how to handle errors
                    return Error();
                }
            }
            catch (SqlException e)
            {
                //TODO Consider how to handle errors
                ViewBag.Message = e.Message;
                return View(e.ToString());
            }
        }


        public IActionResult Put([FromBody] Student student)
        {
            try
            {
                return Ok(studentServices.Put(student));
            }
            catch (SqlException e)
            {
                //TODO Consider how to handle errors
                ViewBag.Message = e.Message;
                return View(e.ToString());
            }
        }


        //I don't know if this index is necessary
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
