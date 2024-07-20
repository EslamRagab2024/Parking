using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Models;

namespace Parking.Controllers
{
    [Authorize(Roles = "Worker")]
    public class WorkerController : Controller
    {

        private readonly MyDb db;

        public WorkerController(MyDb db) 
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ModelState.AddModelError("PhotoError", "Photo is required.");
                return View();
            }

            using (var httpClient = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    using (var ms = new MemoryStream())
                    {
                        await photo.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(photo.ContentType);
                        content.Add(fileContent, "file", photo.FileName);

                        var response = await httpClient.PostAsync("http://127.0.0.1:5000/upload", content);
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();

                            // Decode Unicode escape sequences and parse JSON
                            var decodedResult = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(result);
                            string? license = decodedResult?["last_detected_license"];


                            // Remove the first character from the license
                            license = license.Substring(1);

                            // Find the user by the detected license
                            var resultUser = await db.Bookings.FirstOrDefaultAsync(x => x.License == license);


                            // Redirect to DisplayResult action with the found user
                            return RedirectToAction("Display",resultUser);
                        }
                        else
                        {
                            ModelState.AddModelError("PhotoError", "Error uploading photo.");
                        }
                    }
                }
            }

            return RedirectToAction("Display");
        }

        public IActionResult Display(Booking model) 
        {
            
            return View(model);
        }
    }
}
