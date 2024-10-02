using BackApi.Models;
using BackApi.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaControlle : ControllerBase
    {
        private readonly IPeliculaRepositorio _peliculaRepositorio;
        public PeliculaControlle(IPeliculaRepositorio peliculaRepositorio)
        {
            _peliculaRepositorio = peliculaRepositorio;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_peliculaRepositorio.ObtenerTodas());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_peliculaRepositorio.ObtenerPorId(id));
        }
        [HttpPost]
        public IActionResult Post(Pelicula pelicula)
        {
            pelicula.estreno = true;
            if (string.IsNullOrEmpty(pelicula.titulo) || string.IsNullOrEmpty(pelicula.director)
                || pelicula.anio == 0 || pelicula.id_genero == 0)
            {
                return BadRequest("Faltan datos");
            }
            if (_peliculaRepositorio.Guardar(pelicula))
            {
                return Ok("Pelicula guardada");
            }
            return BadRequest("Error al guardar la pelicula");
        }
        [HttpPut("Modificar Estado")]
        public IActionResult Put(int id)
        {
            var Pelicula = _peliculaRepositorio.ObtenerPorId(id);
            if(Pelicula == null)
            {
                return BadRequest("Pelicula no encontrada");
            }
            Pelicula.estreno = false;
            if (_peliculaRepositorio.Modificar(id))
            {
                return Ok("Estado modificado");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);

        }
    }
}
