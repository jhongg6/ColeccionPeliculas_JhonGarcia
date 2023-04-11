using coleccion_peliculas.Models;

namespace coleccion_peliculas.Servicios
{
    public interface IServices_API
    {
        Task<List<Pelicula>> Lista(int numberPage);
        Task<List<Pelicula>> Buscar(string nombrePelicula, int pageNumber);
        Task<int> Resultados(string nombrePelicula);
        Task<PeliculaDetalles> Detalles(string idIMDb);

    }
}
