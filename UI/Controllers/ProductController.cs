using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UI.Models;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;
        Uri baseAddress = new Uri("https://localhost:7106/api");
        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            HttpResponseMessage response =_client.GetAsync(_client.BaseAddress + "/products/GetAll").Result;
            
            if (response.IsSuccessStatusCode)
            {
                string data =response.Content.ReadAsStringAsync().Result;
                products=JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/products/Create", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product Created.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Product not created";
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                ProductViewModel product = new ProductViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/products/Get/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<ProductViewModel>(data);
                    return View();
                }
                else
                {
                    TempData["errorMessage"] = "Failed to get product"; 
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/products/Update/{model.Id}", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product Created.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Product not created";
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

    }
}
