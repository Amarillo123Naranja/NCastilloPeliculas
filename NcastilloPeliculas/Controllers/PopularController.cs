using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using PLMVC.Models;

namespace PLMVC.Controllers
{
    public class PopularController : Controller
    {
        public IActionResult GetAll()
        {
            Models.Pelicula peli = new Models.Pelicula();

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/movie/popular");

                var responseTask = client.GetAsync("?api_key=55e420e85b712653543782b421452edf&&language=es-MX");

                responseTask.Wait();

                var resultServicio = responseTask.Result;   

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<Models.Root>();
                    readTask.Wait();

                    peli.Peliculas = new List<Object>();

                    foreach (var resultPelicula in readTask.Result.results)
                    {
                        Pelicula pelicula = new Pelicula();
                        
                       // pelicula.backdrop_path = resultPelicula.backdrop_path;

                        pelicula.poster_path = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultPelicula.poster_path;
                             
                        pelicula.title = resultPelicula.title;  

                        pelicula.original_title = resultPelicula.original_title;

                        pelicula.id = resultPelicula.id;

                        pelicula.overview = resultPelicula.overview;   
                        
                        pelicula.release_date = resultPelicula.release_date;    

                        pelicula.popularity = resultPelicula.popularity;

                       // PLMVC.Models.Pelicula resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Pelicula>(resultPelicula.ToString());
                       peli.Peliculas.Add(pelicula);    
                    }

                }

            }

            return View(peli);

        }

        public IActionResult Favorito(int IdPelicula)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://api.themoviedb.org/3/");

                var json = new
                {
                    media_type = "movie",
                    media_id = "IdMovie",
                    favorite = true
                };

                var postTask = client.PostAsJsonAsync("account/20522288/favorite?api_key=55e420e85b712653543782b421452edf&session_id=ad8b367ae2689ec8f22bf0eae5ebdda98cb84ff9/", json);

                postTask.Wait();

                var resultMovie = postTask.Result;

                if(resultMovie.IsSuccessStatusCode) 
                {
                    ViewBag.Mensaje = "Pelicula agregada a favoritos";
 
            
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al agregar la pelicula";

                }

            }

            return RedirectToAction("GetAll", "Popular");
        }



    }
}
