using BookStoreMVC.DTOs.BookDto;
using BookStoreMVC.DTOs.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Controllers
{
    public class BookController : Controller
    {
        public async Task<IActionResult>Index()
        {
            BookListDto listDto;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44311/admin/api/books");
                var responseStr = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<BookListDto>(responseStr);
                    return View(listDto);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreateBook()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateBook(BookCreateDto bookDto)
        {
            if (!ModelState.IsValid) return View();
            using (HttpClient client = new HttpClient())
            {
                byte[] byteArr = null;

                if (bookDto.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        bookDto.ImageFile.CopyTo(ms);
                        byteArr = ms.ToArray();
                    }
                }
                var byteArrContent = new ByteArrayContent(byteArr);
                byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(bookDto.ImageFile.ContentType);
                var multipartContent = new MultipartFormDataContent();
                //multipartContent.Add(new StringContent(JsonConvert.SerializeObject(authorDto.Id), Encoding.UTF8, "application/json"));
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookDto.Name), Encoding.UTF8, "application/json"), "Name");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookDto.Price), Encoding.UTF8, "application/json"), "Price");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookDto.Cost), Encoding.UTF8, "application/json"), "Cost");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookDto.IsDeleted), Encoding.UTF8, "application/json"), "IsDeleted");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookDto.DisplayStatus), Encoding.UTF8, "application/json"), "DisplayStatus");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookDto.AuthorId), Encoding.UTF8, "application/json"), "AuthorId");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookDto.GenreId), Encoding.UTF8, "application/json"), "GenreId");
                multipartContent.Add(byteArrContent, "ImageFile", bookDto.ImageFile.FileName);

                string endpoint = "https://localhost:44311/admin/api/books";


                using (var Response = await client.PostAsync(endpoint, multipartContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                       return RedirectToAction("Index", "Book"); ;
                    }
                }
            }
            //helekilik
            return RedirectToAction("Index", "Book"); ;
        }


        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync("https://localhost:44311/admin/api/books/" + id.ToString());
                var responseStr = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index", "Book");
                }
            }
            return RedirectToAction("Index", "Book");
        }

    }
}
