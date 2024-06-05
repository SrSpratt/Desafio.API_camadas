using Desafio.Consumer.Models;
using Desafio.Consumer.Models.Dtos;
using Desafio.Consumer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Desafio.Consumer.Services.Filters;

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

        public async Task<IActionResult> LoginHandler([FromForm] Login user)
        {
            LoginResponse fromDataBase = await PostUserInfo(user);
            if (ModelState.IsValid && fromDataBase is not null && fromDataBase.Verified)
            {
                await _authenticationMVC.Login(HttpContext, fromDataBase);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "administrator, employee")]
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
            string url = $"{_endpointGetter.BaseUrl}{User.Identity.Name}"; //vou tirar isso e colocar uma variável
            ViewBag.Role = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) // retorna verdadeiro para no content também
            {
                string content = await response.Content.ReadAsStringAsync();
                result = string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            }
            return View(result);
        }

        private async Task<User> GetUserInfo(int id)
        {
            User result = null;
            string url = $"{_endpointGetter.BaseUrl}{id}";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) // retorna verdadeiro para no content também
            {
                string content = await response.Content.ReadAsStringAsync();
                result = string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
            return result;
        }

        private async Task<LoginResponse> PostUserInfo(Login user)
        {
            LoginResponse result = null;
            string url = $"{_endpointGetter.BaseUrl}login";
            
            string json = JsonSerializer.Serialize(user, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, byteContent);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<LoginResponse>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }

            return result;
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateHandler([Bind("Name, Password, Email, Role")]User user)
        {
            string url = $"{_endpointGetter.BaseUrl}";
            string json = JsonSerializer.Serialize(user, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue ("application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, byteContent);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(null, "Error while processing the solicitation");
            }

            return RedirectToAction("Users");
        }
        
        public async Task<IActionResult> Details(int id)
        {
            User result = null;
            string url = $"{_endpointGetter.GenerateEndpoint(id.ToString())}";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
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
        public IActionResult Error(string Message, string ReasonPhrase, string StatusCode)
        {
            return View(new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = Message,
                ReasonPhrase = ReasonPhrase,
                StatusCode = StatusCode
            });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await GetUserInfo(id);
            return View(result);
        }

        public async Task<IActionResult> DeleteHandler(int id)
        {
            string url = $"{_endpointGetter.BaseUrl}{id}";
            HttpResponseMessage response = await httpClient.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(null, "Error");
            }
            return RedirectToAction("Users");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await GetUserInfo(id);
            return View(result);
        }

        public async Task<IActionResult> EditHandler([FromForm]User user)
        {
            string url = $"{_endpointGetter.BaseUrl}{user.Id}";
            string json = JsonSerializer.Serialize(user, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(url, byteContent);

            if (!response.IsSuccessStatusCode)
            {
                string statusCode = ((int)response.StatusCode).ToString();
                string typeError = response.ReasonPhrase;
                string message = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", new { Message = message, ReasonPhrase = typeError, StatusCode =statusCode});
            }

            return RedirectToAction("Users");
        }
    }
}
