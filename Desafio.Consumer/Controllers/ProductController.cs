using Desafio.Consumer.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Desafio.Consumer.Controllers
{
    public class ProductController : Controller
    {
        private readonly string ENDPOINT = "https://localhost:44328/api/Product/";
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

        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Product result = await Search(id);
                return View(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        //O bind garante que tudo que não foi referenciado recebe 0
        public async Task<IActionResult> CreateHandler([Bind("Description, Name, SaleValue, Supplier, Value, Category, ExpirationDate")] Product product)
        {
            try
            {
                string json = JsonConvert.SerializeObject(product);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                string url = $"{ENDPOINT}";
                HttpResponseMessage response = await httpClient.PostAsync(url, byteContent);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await Search(id);

            return View(product);
        }

        public async Task<IActionResult> EditHandler([Bind("Code, Description, Name, SaleValue, Supplier, Value, Category, ExpirationDate")] Product product)
        {
            try
            {
                string json = JsonConvert.SerializeObject(product);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue ("application/json");
                string url = $"{ENDPOINT}{product.Code}";
                HttpResponseMessage response =  await httpClient.PutAsync(url, byteContent);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<Product> Search(int id)
        {
            try
            {
                Product result = null;
                string url = $"{ENDPOINT}{id}";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Product>(content);
                }

                return result;

            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
