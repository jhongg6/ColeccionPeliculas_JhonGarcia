using coleccion_peliculas.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace coleccion_peliculas.Servicios
{
    public class Service_API : IServices_API
    {
        private static string _baseurl;
        private static string _apikey;

        public Service_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
            _apikey = builder.GetSection("ApiSettings:apiKey").Value;
        }

        public async Task<List<Pelicula>> Lista(int pageNumber)
        {
            List<Pelicula> lista = new List<Pelicula>();

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"?apikey={_apikey}&s=taxi&page={pageNumber}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Respuesta_API>(json_respuesta);
                lista = resultado.Search;
            }

            return lista;
        }

        public async Task<List<Pelicula>> Buscar(string nombrePelicula, int pageNumber)
        {
            List<Pelicula> lista = new List<Pelicula>();

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"?apikey={_apikey}&s={nombrePelicula}&page={pageNumber}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Respuesta_API>(json_respuesta);
                lista = resultado.Search;
            }

            return lista;
        }

        public async Task<int> Resultados(string nombrePelicula)
        {
            int totResultados = 0;

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"?apikey={_apikey}&s={nombrePelicula}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Respuesta_API>(json_respuesta);
                totResultados = int.Parse(resultado.TotalResults);
            }

            return totResultados;
        }

        public async Task<PeliculaDetalles> Detalles(string idIMDb)
        {
            PeliculaDetalles detallesPelicula = new PeliculaDetalles();

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"?apikey={_apikey}&i={idIMDb}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<PeliculaDetalles>(json_respuesta);
                detallesPelicula = resultado;
            }

            return detallesPelicula;
        }

    }
}
