using IyiOlusAdminPanel.Models.Contacts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace IyiOlusAdminPanel.Controllers
{
    [Route("Contact")]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index(int pageIndex = 0, int pageSize = 20)
        {
            var client = _httpClientFactory.CreateClient();

            var responseString = await client.GetStringAsync(Constants.ApiRoot + $"Contacts?PageSize={pageSize}&PageIndex={pageIndex}");
            var apiResponse = JsonConvert.DeserializeObject<ContactResponse>(responseString);

            var contacts = apiResponse.Items;

            ViewData["CurrentPage"] = apiResponse?.pageNumber ?? 1;
            ViewData["TotalPages"] = apiResponse?.totalPages ?? 1;
            return View(contacts);
        }

        [HttpGet("{id}")]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(Constants.ApiRoot + $"Contacts/{id}");
            var contact = await response.Content.ReadFromJsonAsync<Contact>();

            return View(contact);

        }
    }
}
