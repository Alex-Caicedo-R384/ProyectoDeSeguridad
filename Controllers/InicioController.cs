using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoDeSeguridad.Models;
using System.Diagnostics;

namespace ProyectoDeSeguridad.Controllers
{
    public class InicioController : Controller
    {

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
                ViewBag.DnsServiceResponse = dnsApiResponse;


                var sslApiResponse = await CallSslCertificateApi(model.Domain);
                TempData["SslApiResponse"] = JsonConvert.SerializeObject(sslApiResponse);
                ViewBag.SSLCertificateResponse = sslApiResponse;


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

            if (TempData["SslApiResponse"] != null)
            {
                var sslApiResponseJson = TempData["SslApiResponse"].ToString();
                var sslApiResponse = JsonConvert.DeserializeObject<SSLCertificateResponse.Rootobject>(sslApiResponseJson);

                return View(sslApiResponse);
            }

            return View();
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

        private async Task<SSLCertificateResponse.Rootobject> CallSslCertificateApi(string domain)
        {
            string apiUrl = BuildApiUrl("SSLCertificateAPI", domain);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Respuesta de la API sin Serializar: {responseBody}"); 
                    return JsonConvert.DeserializeObject<SSLCertificateResponse.Rootobject>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error al llamar a la API {response.RequestMessage.RequestUri}: {response.ReasonPhrase}"); // Imprime el error en la consola
                    throw new Exception($"Error al llamar a la API {response.RequestMessage.RequestUri}: {response.ReasonPhrase}");
                }
            }
        }


        private static readonly Dictionary<string, string> ApiUrls = new Dictionary<string, string>
        {
            { "DnsLookupAPI", "https://networkcalc.com/api/dns/lookup/{domain}" },
            { "DomainReputationAPI", "https://domain-reputation.whoisxmlapi.com/api/v2?apiKey={apiKey}&domainName={domain}" },
            { "WebCategorizationAPI", "https://website-categorization.whoisxmlapi.com/api/v3?apiKey={apiKey}&url={url}" },
            { "SSLCertificateAPI", "https://ssl-certificates.whoisxmlapi.com/api/v1?apiKey={apiKey}&domainName={domain}" }
        };

        private string ApiKey = "at_mvdRfgOl7UEnKpvNXOHm75Xx6YcOH";
        private string ApiKey2 = "at_DkvYKL2PmvGhnZjOATUBQQ3GJGt0Q";
        private string BuildApiUrl(string apiName, string domain)
        {
            string apiUrlPattern = ApiUrls[apiName];

            if (apiUrlPattern.Contains("{apiKey}"))
            {
                string apiKey = apiName switch
                {
                    "DomainReputationAPI" => ApiKey,
                    "WebCategorizationAPI" => ApiKey2,
                    "SSLCertificateAPI" => ApiKey,
                    _ => throw new ArgumentException("Se necesita una ApiKey"),
                };

                string apiUrl = apiUrlPattern.Replace("{domain}", domain)
                                             .Replace("{apiKey}", apiKey);

                return apiUrl;
            }
            else
            {
                string apiUrl = apiUrlPattern.Replace("{domain}", domain);

                return apiUrl;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Evaluar()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
