using Microsoft.AspNetCore.Mvc;

namespace WebLibrary.Controllers
{
    public class FormsController : Controller
    {
        public IActionResult BasicFroms()
        {
            return View();
        }

        public IActionResult Advanced()
        {
            return View();
        }

        public IActionResult Wizard()
        {
            return View();
        }

        public IActionResult FileUpload()
        {
            return View();
        }


        public IActionResult TextEditor()
        {
            return View();
        }

        public IActionResult Markdown()
        {
            return View();
        }

        public IActionResult Autocomplete()
        {
            return View();
        }
    }
}