using Microsoft.AspNetCore.Mvc;
using MvcApiPeliculasAWS.Models;
using MvcApiPeliculasAWS.Service;

namespace MvcApiPeliculasAWS.Controllers
{
    public class PeliculasController : Controller
    {
        private ServiceApiPeliculas service;

        public PeliculasController(ServiceApiPeliculas service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Pelicula> pelis = await this.service.GetPeliculasAsync();
            return View(pelis);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string actor)
        {
            List<Pelicula> pelis = await this.service.FindPeliculasActorAsync(actor);
            return View(pelis);
        }
        public async Task<IActionResult> Details(int id)
        {
            Pelicula peli = await this.service.FindPeliculaAsync(id);
            return View(peli);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Pelicula pelicula)
        {
            await this.service.CreatePeliculaAsync(pelicula);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Pelicula pelicula = await this.service.FindPeliculaAsync(id);
            return View(pelicula);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Pelicula pelicula)
        {
            await this.service.UpdatePeliculaAsync(pelicula);
            return RedirectToAction("Details", new { id = pelicula.IdPelicula });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePeliculaAsync(id);
            return RedirectToAction("Index");
        }
    }
}
