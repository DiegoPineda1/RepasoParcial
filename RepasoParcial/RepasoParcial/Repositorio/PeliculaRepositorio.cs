using BackApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackApi.Repositorio
{
    public interface IPeliculaRepositorio
    {
        bool Guardar(Pelicula pelicula);
        bool Modificar(int id);
        bool Eliminar(int id);
        List<Pelicula> ObtenerTodas();
        Pelicula ObtenerPorId(int id);

    }
    public class PeliculaRepositorio : IPeliculaRepositorio
    {
        private readonly db_cineContext _context;
        public PeliculaRepositorio(db_cineContext db_CineContext)
        {
            _context = db_CineContext;
        }

        public bool Eliminar(int id)
        {
            if(_context.Peliculas.Find(id) == null)
            {
                return false;
            }
            _context.Peliculas.Remove(_context.Peliculas.Find(id));
            _context.SaveChanges();
            return true;
        }

        public bool Guardar(Pelicula pelicula)
        {
            if(pelicula == null)
            {
                return false;
            }
            _context.Peliculas.Add(pelicula);
            _context.SaveChanges();
            return true;
        }

        public bool Modificar(int id)
        {
            if(id == 0)
            {
                return false;
            }
            _context.Peliculas.Update(_context.Peliculas.Find(id));
            _context.SaveChanges();
            return true;
        }

        public Pelicula ObtenerPorId(int id)
        {
            if(_context.Peliculas.Find(id) == null)
            {
                return null;
            }
            return _context.Peliculas.Find(id);
        }

        public List<Pelicula> ObtenerTodas()
        {
            if(_context.Peliculas.Count() == 0)
            {
                return null;
            }
            return _context.Peliculas.Where(x => x.estreno == true).ToList();
        }
    }
}
