using Desafio.Consumer.Models;
using Desafio.Consumer.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Desafio.Consumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string ENDPOINT = "";
        private readonly HttpClient httpClient = null;

        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            ENDPOINT = _configuration["App_url"];
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ENDPOINT);
        }

        public IActionResult Index()
        {
            ViewBag.result = "not authenticated";
            if (TempData["auth"] != null)
            {
                ViewBag.result = TempData["auth"];
            }
            return View();
        }

        public async Task<IActionResult> LoginHandler([FromForm] User user)
        {
            if (ModelState.IsValid)
                TempData["auth"] = "authenticated";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExhibitUser()
        {
            User result = null;
            string url = $"{ENDPOINT}Login/nome"; //vou tirar isso e colocar uma variável
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            }
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
