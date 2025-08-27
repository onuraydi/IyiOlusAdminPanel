using IyiOlusAdminPanel.Models.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IyiOlusAdminPanel.Controllers
{
    [Route("Question")]
    public class QuestionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public QuestionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index(int pageIndex = 0, int PageSize = 20)
        {
            var token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseString = await client.GetStringAsync(Constants.ApiRoot + $"Questions?PageIndex={pageIndex}&PageSize={PageSize}");


            var apiResponse = JsonConvert.DeserializeObject<QuestionResponse>(responseString);
            var questions = apiResponse.Items;

            // ViewData ile pagination bilgilerini gönder
            ViewData["CurrentPage"] = apiResponse?.pageNumber ?? 1;
            ViewData["TotalPages"] = apiResponse?.totalPages ?? 1;

            return View(questions);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateQuestionDto createQuestionDto)
        {
            var token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var payload = new
            {
                request = createQuestionDto
            };

            var response = await client.PostAsJsonAsync(Constants.ApiRoot + "Questions", payload);

            return RedirectToAction("Index", "Question");
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(Constants.ApiRoot + $"Questions/{id}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var question = await response.Content.ReadFromJsonAsync<UpdateQuestionDto>(options);

            return View(question);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task <IActionResult> Update(UpdateQuestionDto question)
        {
            var token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var payload = new
            {
                request = question
            };
            var response = await client.PutAsJsonAsync(Constants.ApiRoot + $"Questions", payload);

            return RedirectToAction("Index", "Question");
        }

        [HttpDelete("{id}")]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync(Constants.ApiRoot + $"Questions/{id}");
            return RedirectToAction("Index", "Question");
        }

    }
}
