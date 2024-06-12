using Desafio.Consumer.Models.Dtos;
using Desafio.Consumer.Services;
using Desafio.Consumer.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Desafio.Consumer.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient httpClient = null;
        private readonly EndpointGetter _endpointGetter;

        public ProductController(EndpointGetter endpointGetter)
        {
            this.httpClient = new HttpClient();
            _endpointGetter = endpointGetter;
            _endpointGetter.SetBaseUrl(1);
            httpClient.BaseAddress = new Uri(_endpointGetter.BaseUrl);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Product> products = null;
            
            var url = _endpointGetter.GenerateEndpoint("");
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(content);      
            }
            return View(products);
        }

        public async Task<IActionResult> ShowName(string type)
        {
            List<Product> products = null;

            var url = _endpointGetter.GenerateEndpoint("");
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(content);
                if (!string.IsNullOrEmpty(type) && (type != "Category"))
                {
                    products = products.Where(product => product.Category == type).ToList();
                }
            }
            return PartialView("_ShowListPartial",products);
        }

        public async Task<IActionResult> ExhibitName(string name)
        {
            List<Product> products = null;

            var url = _endpointGetter.GenerateEndpoint("");
            HttpResponseMessage response = await httpClient.GetAsync(url); 
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(content);
                if (!string.IsNullOrEmpty(name))
                {
                    products = products.Where(product => product.Name.Contains(name)).ToList();
                }

            }
            return PartialView("_ShowListPartial", products);

        }

        public async Task<IActionResult> Get(int id)
        {
            Product result = await Search(id);
            return View(result);
        }



        [Route("Operations/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<List<StockOperation>>> GetOperations(int id)
        {
            List<StockOperation> operations = new List<StockOperation>();
            string url = _endpointGetter.GenerateCrossEndpoint("GetAllOperations", 4);
            url += $"{id}";
            ViewBag.id = id;
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                operations = JsonConvert.DeserializeObject<List<StockOperation>>(content);
            }
            return View(operations);
        }

        [Authorize(Roles = "administrator")]
        [RestoreTempModelState]
        public async Task<IActionResult> Create()
        {
            ProductViewModel model = new ProductViewModel();
            List<Category> categories = null;

            var url = _endpointGetter.GenerateCrossEndpoint("", 2);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(content);
                model.categories = categories;
            }

            return View(model);
        }

        [Authorize(Roles = "administrator")]
        [SetTempModelState]
        //O bind garante que tudo que não foi referenciado recebe 0
        public async Task<IActionResult> CreateHandler([Bind("Description, Name, SaleValue, Supplier, Value, Category, ExpirationDate, Amount, Operation")] Product product)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(product);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var url = _endpointGetter.GenerateEndpoint("");
                HttpResponseMessage response = await httpClient.PostAsync(url, byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new ArgumentException("There was a problem while processing the solicitation!\n\n" + message);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        [Authorize(Roles = "administrator")]
        [RestoreTempModelState]
        public async Task<IActionResult> Edit(int id)
        {

            var isvalid = ModelState.IsValid;

            Product product = await Search(id);
            ProductViewModel productModel = product.toProduct();
            List<Category> categories = null;

            var url = _endpointGetter.GenerateCrossEndpoint("", 2);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
               string content = await response.Content.ReadAsStringAsync();
               categories = JsonConvert.DeserializeObject<List<Category>>(content);
               productModel.categories = categories;
            }

            return View(productModel);
        }

        [Authorize(Roles = "administrator, employee")]
        [SetTempModelState]
        [HttpPost]
        public async Task<IActionResult> EditHandler([FromForm] ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                Product product = productModel.toProduct();
                if (string.Equals(product.Operation.OperationType, "Add"))
                    product.Amount += product.Operation.OperationAmount;
                else
                    product.Amount -= product.Operation.OperationAmount;
                string json = JsonConvert.SerializeObject(product);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var url = _endpointGetter.GenerateEndpoint($"{product.Code}");
                HttpResponseMessage response = await httpClient.PutAsync(url, byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new ArgumentException("There was a problem while processing the solicitation!" + message);
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new { id = productModel.Code });
        }

        [Authorize(Roles = "employee")]
        public async Task<IActionResult> ChangeStock(int id)
        {
            Product product = await Search(id);

            return View(product);
        }

        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await Search(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> DeleteHandler(string Code) //TEM que nomeado EXATAMENTE como na tag do formulário que redireciona para cá
        {
            int id = Int32.Parse(Code);
            var url = _endpointGetter.GenerateEndpoint($"{id}");

            HttpResponseMessage response = await httpClient.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("There was a problem while processing the solicitation!");

            return RedirectToAction("Index");
        }

        private async Task<Product> Search(int id)
        {
            try
            {
                Product result = null;
                var url = _endpointGetter.GenerateEndpoint($"{id}");
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

        private async Task<Product> GetCategoriesList()
        {
            try
            {
                Product result = null;
                var url = _endpointGetter.GenerateEndpoint($"");
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Product>(content);
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
