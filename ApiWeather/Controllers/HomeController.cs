using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ApiWeather.Models;
using Newtonsoft.Json;

public class HomeController : Controller
{
    private readonly string WeatherApiBaseUrl;
    private readonly string WeatherApiKey;

    public HomeController()
    {
        WeatherApiBaseUrl = ConfigurationManager.AppSettings["WeatherApiBaseUrl"];
        WeatherApiKey = ConfigurationManager.AppSettings["WeatherApiKey"];
    }

    public ActionResult Index() {

        return View(new PronosticoCiudad());
        
    }


    [HttpPost]
    public async Task<ActionResult> ObtenerTemperatura(PronosticoCiudad model)
    {
        if (string.IsNullOrEmpty(WeatherApiBaseUrl) || string.IsNullOrEmpty(WeatherApiKey))
        {
            ViewBag.Error = "Las configuraciones de la API no están configuradas correctamente.";
            return View("Index", model);
        }

        using (var httpClient = new HttpClient())
        {
            try
            {
                var apiPronostico = $"{WeatherApiBaseUrl}forecast.json?key={WeatherApiKey}&q={model.nombreCiudad}&days={6}";
                var response = await httpClient.GetAsync(apiPronostico);


                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                   
                    var pronostico = JsonConvert.DeserializeObject(data);

                    ViewBag.Temperatura = pronostico;

                   
                }
                else
                {
                    ViewBag.Error = "Hubo un error al obtener los datos de temperatura.";
                }
            }
            catch (Exception ex)
            {
               
                ViewBag.Error = "Hubo un error al procesar la solicitud: " + ex.Message;
            }
        }

        return View("Index", model);
    }
}

