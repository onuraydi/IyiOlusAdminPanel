using IyiOlusAdminPanel.Models.Login;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace IyiOlusAdminPanel.Controllers
{
    [Route("Auth")]
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var client = _httpClientFactory.CreateClient();

            var payload = new
            {
                request = loginDto
            };

            var response = await client.PostAsJsonAsync(Constants.ApiRoot + "Auth/Login", payload);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                return View(loginDto);
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseBody);

            if (tokenResponse!= null && !string.IsNullOrEmpty(tokenResponse.token))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = loginDto.RememberMe
                        ? DateTimeOffset.UtcNow.AddDays(7)
                        : DateTimeOffset.UtcNow.AddHours(1)
                };

                Response.Cookies.Append("Token", tokenResponse.token, cookieOptions);

                if (!string.IsNullOrEmpty(tokenResponse.refreshToken))
                {
                    Response.Cookies.Append("refreshToken", tokenResponse.refreshToken, cookieOptions);
                }


                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Token alınamadı!");
            return View(loginDto);
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("refreshToken");

            return RedirectToAction("Login","Auth");
        }
    }
}
