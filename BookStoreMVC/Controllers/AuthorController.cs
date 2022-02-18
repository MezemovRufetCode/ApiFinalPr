using BookStoreMVC.DTOs.AuthorDtos;
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
    public class AuthorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            AuthorListDto listDto;

            //List<AuthorListItemDto> authors = new List<AuthorListItemDto>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44311/admin/api/author");
                var responseStr = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listDto = JsonConvert.DeserializeObject<AuthorListDto>(responseStr);
                    return View(listDto);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateAuthor(AuthorListItemDto authorDto)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] byteArr = null;

                if (authorDto.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        authorDto.ImageFile.CopyTo(ms);
                        byteArr = ms.ToArray();
                    }
                }
                var byteArrContent = new ByteArrayContent(byteArr);
                byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(authorDto.ImageFile.ContentType);
                var multipartContent = new MultipartFormDataContent();
                //multipartContent.Add(new StringContent(JsonConvert.SerializeObject(authorDto.Id), Encoding.UTF8, "application/json"));
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(authorDto.Name), Encoding.UTF8, "application/json"), "Name");
                multipartContent.Add(byteArrContent, "ImageFile", authorDto.ImageFile.FileName);

                string endpoint = "https://localhost:44311/admin/api/author";


                using (var Response = await client.PostAsync(endpoint, multipartContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync("https://localhost:44311/admin/api/author/" + id.ToString());
                var responseStr = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index", "Author");
                }
            }
            return RedirectToAction("Index", "Author");
        }


        //public async Task<IActionResult> UpdateAuthor(int id)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("https://localhost:44311/admin/api/author/" + id.ToString());
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {

        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(int id, AuthorListItemDto authorDto)
        //{
        //    AuthorListItemDto exauthordto = null;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        byte[] byteArr = null;

        //        if (authorDto.ImageFile.Length > 0)
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                authorDto.ImageFile.CopyTo(ms);
        //                byteArr = ms.ToArray();
        //            }
        //        }
        //        var byteArrContent = new ByteArrayContent(byteArr);
        //        byteArrContent.Headers.ContentType = MediaTypeHeaderValue.Parse(authorDto.ImageFile.ContentType);
        //        var multipartContent = new MultipartFormDataContent();
        //        //multipartContent.Add(new StringContent(JsonConvert.SerializeObject(authorDto.Id), Encoding.UTF8, "application/json"));
        //        multipartContent.Add(new StringContent(JsonConvert.SerializeObject(authorDto.Name), Encoding.UTF8, "application/json"), "Name");
        //        multipartContent.Add(byteArrContent, "ImageFile", authorDto.ImageFile.FileName);

        //        string endpoint = "https://localhost:44311/admin/api/author";


        //        using (var Response = await client.PostAsync(endpoint, multipartContent))
        //        {
        //            if (Response.StatusCode == System.Net.HttpStatusCode.Created)
        //            {
        //                return RedirectToAction("Index", "Author");
        //            }
        //            else
        //            {
        //                return BadRequest();
        //            }
        //        }
        //    }
        //    //helekilik
        //    return View("Index", "Author");
        //}

    }
}
