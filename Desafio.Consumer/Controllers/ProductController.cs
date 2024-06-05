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
            try
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

            } catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
            }

        }

        public async Task<IActionResult> ShowName(string type)
        {
            try
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
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
            }
        }

        public async Task<IActionResult> ExhibitName(string name)
        {
            try
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
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
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
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
            }
        }

        public async Task<IActionResult> Create()
        {
            try
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
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
            }
        }

        //O bind garante que tudo que não foi referenciado recebe 0
        public async Task<IActionResult> CreateHandler([Bind("Description, Name, SaleValue, Supplier, Value, Category, ExpirationDate, Amount")] Product product)
        {
            try
            {
                string json = JsonConvert.SerializeObject(product);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var url = _endpointGetter.GenerateEndpoint("");
                HttpResponseMessage response = await httpClient.PostAsync(url, byteContent);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Error while processing the solicitation");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
            }
        }

        [RestoreTempModelState]
        public async Task<IActionResult> Edit(int id)
        {

            var isvalid = ModelState.IsValid;

            Product product = await Search(id);
            ProductViewModel productModel = product.toProduct();
            try
            {
                List<Category> categories = null;

                var url = _endpointGetter.GenerateCrossEndpoint("", 2);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(content);
                    productModel.categories = categories;
                }
                /*
                if (TempData["Errors"] is not null)
                {
                    var modelStateString = TempData["Errors"].ToString();
                    var listError = JsonConvert.DeserializeObject<Dictionary<string, string>>(modelStateString);
                    var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
                    foreach (var item in listError)
                    {
                        modelState.AddModelError(item.Key, item.Value ?? "");
                    }
                    TempData["Errors"] = JsonConvert.SerializeObject(listError);

                    ViewData.ModelState.Merge(modelState);
                    if (!ModelState.IsValid)
                    {
                        return View(productModel);
                    }
                }
                */

                return View(productModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
            }
        }

        [SetTempModelState]
        [HttpPost]
        public async Task<IActionResult> EditHandler([FromForm] ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                Product product = productModel.toProduct();
                try
                {
                    string json = JsonConvert.SerializeObject(product);
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var url = _endpointGetter.GenerateEndpoint($"{product.Code}");
                    HttpResponseMessage response = await httpClient.PutAsync(url, byteContent);

                    if (!response.IsSuccessStatusCode)
                        ModelState.AddModelError(null, "Error while processing the solicitation");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home", new { Message = ex.Message });
                }
            }
            /*
            var errors = ModelState.Values.SelectMany(modelState => modelState.Errors);
            ModelState.AddModelError("SaleValue", "Error");
            var listError = ModelState.Where(x => x.Value.Errors.Any())
                .ToDictionary(m => m.Key, m => m.Value.Errors
                .Select(s => s.ErrorMessage)
                .FirstOrDefault(s => s != null));
            TempData["Errors"] = JsonConvert.SerializeObject(listError);
            */
            return RedirectToAction("Edit", new { id = productModel.Code });
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await Search(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        public async Task<IActionResult> DeleteHandler(string Code) //TEM que nomeado EXATAMENTE como na tag do formulário que redireciona para cá
        {
            try
            {
                int id = Int32.Parse(Code);
                var url = _endpointGetter.GenerateEndpoint($"{id}");

                HttpResponseMessage response = await httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Error while processing the solicitation");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { Message = ex.Message });
            }
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
