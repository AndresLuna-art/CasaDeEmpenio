using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Cryptography;

namespace CasaDeEmpeño.Controllers
{
    public class LoginController : Controller
    {
        private const string HardcodedUsername = "admin";
        private const string HardcodedPassword = "admin123%";
        private const string EncryptionKey = "supersecretkeythatneedstobelonger";

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string username, string password, string returnUrl = null)
        {
            if (username == HardcodedUsername)
            {
                var decryptedPassword = DecryptAes(password, EncryptionKey);

                if (decryptedPassword == HardcodedPassword)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }

            // Autenticación fallida
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        private static string DecryptAes(string cipherText, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = GenerateKey(key);
                aes.IV = new byte[16]; 

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                var cipherBytes = Convert.FromBase64String(cipherText);

                using (var ms = new MemoryStream(cipherBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static byte[] GenerateKey(string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length == 32) return keyBytes;
            var paddedKeyBytes = new byte[32];
            Buffer.BlockCopy(keyBytes, 0, paddedKeyBytes, 0, Math.Min(keyBytes.Length, paddedKeyBytes.Length));
            return paddedKeyBytes;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
