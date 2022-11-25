using DI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Services;

namespace DI.Controllers
{
    public class HomeController : Controller
    {
        //dont create object of service here. Instead, use dependency injection to get the object of service
        //private readonly CitiesService _citiesService;

        //constructor which creates object of service
        //public HomeController()
        //{
        //    //create object of Cities Service class
        //    _citiesService = new CitiesService();
        //}

        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;

        //but not we cannot create object of service here. Because we are using Contracts instead which is interface and interface cannot have objects as it has incomplete methods, so we will use Dependency Injection or Inversion of Control
        public HomeController(ICitiesService citiesService1, ICitiesService citiesService2, ICitiesService citiesService3, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IOptions<WeatherApiOptions> weatherApiOptions)
        {
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
            _env = webHostEnvironment;
            _configuration = configuration;
            _weatherApiOptions = weatherApiOptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            //get cities from service
            var cities = _citiesService1.GetCities();

            ViewBag.ServiceInstanceId1 = _citiesService1.ServiceInstanceId;
            ViewBag.ServiceInstanceId2 = _citiesService2.ServiceInstanceId;
            ViewBag.ServiceInstanceId3 = _citiesService3.ServiceInstanceId;

            //pass cities to view to become Strongly Typed View
            return View(cities);
        }

        //If we do not want the object of whole class and only want some specific methods of it we can use method injection as well

        //[Route("/method-injection")]
        //public IActionResult method_injection([FromServices] ICitiesService _citiesService)
        //{
        //    //get cities from service
        //    var cities = _citiesService.GetCities();
        //    //pass cities to view to become Strongly Typed View
        //    return View(cities);
        //}

        //accessing the environment property if not in Program.cs file
        private readonly IWebHostEnvironment _env;

        [Route("/about")]
        public IActionResult About()
        {
            //we can access the environment variables from here
            if (_env.IsDevelopment())
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private readonly IConfiguration _configuration;

        private readonly WeatherApiOptions _weatherApiOptions;

        [Route("/contact")]
        public IActionResult Contact()
        {
            //we can access the configuration variables from here

            ViewBag.configValue = _configuration.GetValue<string>("MyKey");
            ViewBag.innerVlaue = _configuration["MyKey: innerKey"];

            ViewBag.innerValue1 = _configuration.GetSection("MyKey")["innerValue1"];

            WeatherApiOptions weatherApiOpt = _configuration.GetSection("weatherApi").Get<WeatherApiOptions>();

            return View();
        }
    }

}
