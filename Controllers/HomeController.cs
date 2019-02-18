using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FluidTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("<!DOCTYPE html><html><head><title>Page Title</title></head><body><a href=\"/Home/English\" target=\"_blank\">1) Click here first (Continue the execution on Exception)</a><br /><a href=\"/Home/EnglishGlobal\" target=\"_blank\">2) Click here second</a><br /><a href=\"/Home/English\" target=\"_blank\">3) Now the first broken link works because of Global manipulation</a><br /><a href=\"/Home/Swedish\" target=\"_blank\">4) This also works because of the second click otherwise it wouldn't!</a></body></html>", MediaTypeHeaderValue.Parse("text/html"));
        }

        public IActionResult English()
        {
            // This doesn't work

            return Content(EnglishEngine.Render());
        }

        public IActionResult Swedish()
        {
            // This doesn't work

            return Content(SwedishEngine.Render());
        }

        public IActionResult EnglishGlobal()
        {
            // This works but messes up the Global Context

            return Content(EnglishEngine.RenderGlobal());
        }

        public IActionResult SwedishGlobal()
        {
            // This works but messes up the Global Context

            return Content(SwedishEngine.RenderGlobal());
        }
    }
}
