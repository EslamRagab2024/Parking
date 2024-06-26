using Microsoft.AspNetCore.Mvc;

namespace Parking.Controllers
{
    public class WorkerController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile photo)
        {
            // send image to server http://127.0.0.1:5000 and response like this ٨ ٤ ٣ ٢ م س ط

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
                            // Process the response (e.g., save the result to ViewData, TempData, etc.)
                            //ViewData["ServerResponse"] = result;
                            TempData["ServerResponse"] = result;
                            return RedirectToAction("DisplayResult");
                        }
                        else
                        {
                            ModelState.AddModelError("PhotoError", "Error uploading photo.");
                        }
                    }
                }
            }

            return RedirectToAction("DisplayResult");


        }


        public IActionResult DisplayResult()
        {
            if (TempData.ContainsKey("ServerResponse"))
            {
                ViewBag.ServerResponse = TempData["ServerResponse"]?.ToString();
            }
            else
            {
                ViewBag.ServerResponse = "No response from the server.";
            }
            
            return View();
        }

    }
}
