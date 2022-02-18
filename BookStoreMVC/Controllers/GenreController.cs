using BookStoreMVC.DTOs.GenreDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Controllers
{
    public class GenreController : Controller
    {
        public async Task<IActionResult> Index()
        {
            GenreListDto listDto;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44311/admin/api/genre");
                var responseStr = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<GenreListDto>(responseStr);
                    return View(listDto);
                }
            }
            return RedirectToAction("Index", "Home");

        }


        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateGenre(GenreListItemDto genreDto)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(genreDto), Encoding.UTF8, "application/json");
                string endpoint = "https://localhost:44311/admin/api/genre";


                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "Genre");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            //helekilik
            return View("Index", "Genre");
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync("https://localhost:44311/admin/api/genre/" + id.ToString());
                var responseStr = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index", "Genre");
                }
            }
            return RedirectToAction("Index", "Genre");
        }

    }
}
