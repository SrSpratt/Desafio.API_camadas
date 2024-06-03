using Desafio.Consumer.Models;
using Desafio.Consumer.Models.Dtos;
using Desafio.Consumer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace Desafio.Consumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient httpClient = null;
        private readonly EndpointGetter _endpointGetter;
        private readonly AuthenticationMVC _authenticationMVC;
        public HomeController(ILogger<HomeController> logger, EndpointGetter endpointGetter, AuthenticationMVC authenticationMVC)
        {
            _logger = logger;
            httpClient = new HttpClient();
            _endpointGetter = endpointGetter;
            _endpointGetter.SetBaseUrl(2);
            httpClient.BaseAddress = new Uri(endpointGetter.BaseUrl);
            _authenticationMVC = authenticationMVC;
        }

        public IActionResult Index()
        {
            return HttpContext.User.Identity.IsAuthenticated ? RedirectToAction("Index", "Product") : View();
        }

        public async Task<IActionResult> LoginHandler([FromForm] User user)
        {
            User fromDataBase = await GetUserInfo(user);
            if (ModelState.IsValid && fromDataBase is not null && user.Password.Equals(fromDataBase.Password))
            {
                await _authenticationMVC.Login(HttpContext, fromDataBase);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> Users()
        {
            List<User> users = null;
            try
            {
                string url = _endpointGetter.GenerateEndpoint("");
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    users = JsonSerializer.Deserialize<List<User>>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                }
                return users != null ? View(users) : View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { Message = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticationMVC.Logout(HttpContext);
            return RedirectToAction("Index");
        }


        [Authorize]
        public async Task<IActionResult> ExhibitUser()
        {
            User result = null;
            string url = $"{_endpointGetter.BaseUrl}{3}"; //vou tirar isso e colocar uma variável
            ViewBag.Role = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) // retorna verdadeiro para no content também
            {
                string content = await response.Content.ReadAsStringAsync();
                result = string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            }
            return View(result);
        }

        private async Task<User> GetUserInfo(User user)
        {
            User result = null;
            string url = $"{_endpointGetter.BaseUrl}{user.Name}";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) // retorna verdadeiro para no content também
            {
                string content = await response.Content.ReadAsStringAsync();
                result = string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            }
            return result;
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
