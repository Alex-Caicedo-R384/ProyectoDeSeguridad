using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoDeSeguridad.Models;
using System.Diagnostics;

namespace ProyectoDeSeguridad.Controllers
{
    public class InicioController : Controller
    {
        private static readonly Dictionary<string, string> ApiUrls = new Dictionary<string, string>
        {
            { "DnsLookupAPI", "https://networkcalc.com/api/dns/lookup/{domain}" },
            { "DomainReputationAPI", "https://domain-reputation.whoisxmlapi.com/api/v2?apiKey={apiKey}&domainName={domain}" },
            { "WebsiteCategorizationAPI", "https://website-categorization.whoisxmlapi.com/api/v3?apiKey={apiKey}&url={url}" },
            { "SSLCertificateAPI", "https://ssl-certificates.whoisxmlapi.com/api/v1?apiKey={apiKey}&domainName={domain}" }
        };

        private string ApiKey = "at_mvdRfgOl7UEnKpvNXOHm75Xx6YcOH";
        private string ApiKey2 = "at_DkvYKL2PmvGhnZjOATUBQQ3GJGt0Q";

        [HttpGet]
        public IActionResult Inicio(string Domain)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DomainRequest model)
        {
            Console.WriteLine("Dominio recibido en Index: " + model.Domain);

            if (string.IsNullOrEmpty(model?.Domain))
            {
                ModelState.AddModelError("", "Por favor, escriba un dominio.");
                return View();
            }

            try
            {
                var dnsApiResponse = await CallDnsLookupApi(model.Domain);

                TempData["DnsApiResponse"] = JsonConvert.SerializeObject(dnsApiResponse);

                ViewBag.Dominio = model.Domain;

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al llamar a la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Detalles()
        {
            if (TempData["DnsApiResponse"] != null)
            {
                var dnsApiResponseJson = TempData["DnsApiResponse"].ToString();
                var dnsApiResponse = JsonConvert.DeserializeObject<DnsServiceResponse.Rootobject>(dnsApiResponseJson);
                return View(dnsApiResponse);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        private async Task<DnsServiceResponse.Rootobject> CallDnsLookupApi(string domain)
        {
            string apiUrl = BuildApiUrl("DnsLookupAPI", domain);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DnsServiceResponse.Rootobject>(responseBody);
                }
                else
                {
                    throw new Exception($"Error al llamar a la API {response.RequestMessage.RequestUri}: {response.ReasonPhrase}");
                }
            }
        }

        private string BuildApiUrl(string apiName, string domain)
        {
            string apiUrl = ApiUrls["DnsLookupAPI"];
            apiUrl = apiUrl.Replace("{domain}", domain);
            return apiUrl;
        }
    }
}
