using MvcApiPeliculasAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcApiPeliculasAWS.Service
{
    public class ServiceApiPeliculas
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiPeliculas(IConfiguration config)
        {
            this.UrlApi = config.GetValue<string>("ApiUrls:ApiPeliculasAWS");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            string request = "api/peliculas";
            return await this.CallApiAsync<List<Pelicula>>(request);
        }

        public async Task<Pelicula> FindPeliculaAsync(int id)
        {
            string request = "api/peliculas/" + id;
            return await this.CallApiAsync<Pelicula>(request);
        }

        public async Task<List<Pelicula>> FindPeliculasActorAsync(string actor)
        {
            string request = "api/peliculas/PeliculasActor/" + actor;
            return await this.CallApiAsync<List<Pelicula>>(request);
        }

        public async Task CreatePeliculaAsync(Pelicula pelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(pelicula);
                StringContent content = new StringContent(json, this.Header);
                HttpResponseMessage response = await client.PostAsync(this.UrlApi + request, content);
            }
        }

        public async Task UpdatePeliculaAsync(Pelicula pelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(pelicula);
                StringContent content = new StringContent(json, this.Header);
                HttpResponseMessage response = await client.PutAsync(this.UrlApi + request, content);
            }
        }

        public async Task DeletePeliculaAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas/" + id;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(this.UrlApi + request);
            }
        }
    }
}
