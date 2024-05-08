using Desafio.Consumer.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Desafio.Consumer.Controllers
{
    public class ProductController : Controller
    {
        private readonly string ENDPOINT = "https://localhost:44328/api/Product";
        private readonly HttpClient httpClient = null;

        public ProductController()
        {
            this.httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ENDPOINT);
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Product> products = null;

                HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(content);
                }
                else
                {
                    ModelState.AddModelError(null, "Erro ao processar");
                }
                return View(products);

            } catch (Exception ex)
            {
                string message = ex.Message;
                throw ex;
            }

        }
    }
}
