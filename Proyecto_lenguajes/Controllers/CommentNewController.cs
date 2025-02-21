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

        [HttpGet]
        public async Task<int> CheckType(string id)
        {
            int type = 0;

            try
            {
                type = await Task.Run(() => commentNewServices.CheckType(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar el tipo de comentario");
                ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}");
            }

            return type;
        }

        [HttpGet]
        public async Task<Professor> GetProfessorCommentData(string id)
        {
            Professor auxProf = null;

            try
            {
                auxProf = await Task.Run(() => commentNewServices.GetProfessorCommentData(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los datos del profesor");
                ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}");
            }

            return auxProf;
        }

        [HttpGet]
        public async Task<Student> GetStudentCommentData(string id)
        {
            Student auxStudent = null;

            try
            {
                auxStudent = await Task.Run(() => commentNewServices.GetStudentCommentData(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los datos del estudiante");
                ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}");
            }

            return auxStudent;
        }

        [HttpGet]
        public IEnumerable<CommentNew> GetAll(int id)
        {
            IEnumerable<CommentNew> comments = null;

            try
            {
                comments = commentNewServices.GetAll(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los comentarios");
                ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}");
                comments = Enumerable.Empty<CommentNew>();
            }

            return comments;
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
