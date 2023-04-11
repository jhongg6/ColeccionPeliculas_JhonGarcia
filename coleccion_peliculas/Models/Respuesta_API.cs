namespace coleccion_peliculas.Models
{
    public class Respuesta_API
    {
        public List<Pelicula> Search { get; set; }
        public string TotalResults { get; set; }
        public bool Response { get; set; }
    }
}
