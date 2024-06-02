using Desafio.Consumer.Models;
using Desafio.Consumer.Models.Dtos;
using Desafio.Consumer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace Desafio.Consumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient httpClient = null;
        private readonly EndpointGetter _endpointGetter;
        public HomeController(ILogger<HomeController> logger, EndpointGetter endpointGetter)
        {
            _logger = logger;
            httpClient = new HttpClient();
            _endpointGetter = endpointGetter;
            httpClient.BaseAddress = new Uri(endpointGetter.BaseUrl);
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
            if (ModelState.IsValid && user.Password.Equals("senha"))
            {
                List<Claim> userClaim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Name)
                };

                var myId = new ClaimsIdentity(userClaim, "User");
                var userPrincipal = new ClaimsPrincipal(new[] { myId });
                HttpContext.SignInAsync(userPrincipal);
                TempData["auth"] = "authenticated";
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> ExhibitUser()
        {
            User result = null;
            string url = $"{_endpointGetter.BaseUrl}Login/{HttpContext.User.Identity.Name}"; //vou tirar isso e colocar uma variável
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) // retorna verdadeiro para no content também
            {
                string content = await response.Content.ReadAsStringAsync();
                result = string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            }
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string Message)
        {
            return View(new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = Message
            });
        }
    }
}
