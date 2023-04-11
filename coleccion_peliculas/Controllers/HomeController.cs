using coleccion_peliculas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using coleccion_peliculas.Servicios;
using System.Drawing.Printing;

namespace coleccion_peliculas.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServices_API _serviceApi;
        const int PageSize = 10;

        public HomeController(IServices_API serviceApi)
        {
            _serviceApi = serviceApi;
        }

        public async Task<IActionResult> Index(int numeroPag = 1, string nombrePelicula = null)
        {
            List<Pelicula> peliculas;
            List<PeliculaDetalles> peliculaDetalles;
            int totResultados;

            if (string.IsNullOrEmpty(nombrePelicula))
            {
                peliculas = await _serviceApi.Lista(numeroPag);
                peliculaDetalles = await idImdb(peliculas);
                ViewBag.CurrentPage = numeroPag;
                ViewBag.TotalPages = Math.Ceiling((double)peliculas.Count / PageSize);
            }
            else
            {
                peliculas = await _serviceApi.Buscar(nombrePelicula, numeroPag);
                peliculaDetalles = await idImdb(peliculas); peliculaDetalles = await idImdb(peliculas);
                totResultados = await _serviceApi.Resultados(nombrePelicula);
                ViewBag.TotalPages = Math.Ceiling((double)totResultados / PageSize);
                ViewBag.NameMovie = nombrePelicula;
            }

            var peliculasPaginadas = peliculaDetalles.Take(PageSize);

            ViewBag.CurrentPage = numeroPag;

            return View(peliculasPaginadas);

        }

        public async Task<List<PeliculaDetalles>> idImdb(List<Pelicula> peliculas)
        {
            List<PeliculaDetalles> peliculaDetalles = new List<PeliculaDetalles>();

            foreach(var item in peliculas)
            {
                var detalles = await _serviceApi.Detalles(item.imdbID);
                peliculaDetalles.Add(detalles);
            }
            return peliculaDetalles;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}