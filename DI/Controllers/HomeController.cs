using Microsoft.AspNetCore.Mvc;
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

        private readonly ICitiesService _citiesService;

        //but not we cannot create object of service here. Because we are using Contracts instead which is interface and interface cannot have objects as it has incomplete methods, so we will use Dependency Injection or Inversion of Control
        public HomeController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            //get cities from service
            var cities = _citiesService.GetCities();

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


    }
}
