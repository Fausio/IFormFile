using IFormFile.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IFormFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(new FileContent());
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

        [HttpPost]
        public async Task<IActionResult> create(FileContent model)
        {


            if (ModelState.IsValid)
            {
                if (model.Content != null)
                {
                    // the logic here, 
                    string folder = "Files/";
                    folder += model.Content.FileName;

                    // avoid the same name
                    //    folder += model.Content.FileName + Guid.NewGuid().ToString();
                    string serverFolder = Path.Combine(_env.WebRootPath, folder);


                    // save into the the folder
                    await model.Content.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
            }

            return View();
        }

        public IActionResult MultipleFiles(IEnumerable<Microsoft.AspNetCore.Http.IFormFile> files)
        {
            int i = 0;
            foreach (var file in files)
            {
                using (var fileStream = new FileStream(Path.Combine(_env.ContentRootPath, $"file{i++}.png"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult FileInModel(SomeForm someForm)
        {
            using (var fileStream = new FileStream(
                Path.Combine(_env.ContentRootPath, $"{someForm.Name}.png"),
                FileMode.Create,
                FileAccess.Write))
            {
                someForm.File.CopyTo(fileStream);
            }

            return RedirectToAction("Index");
        }
    }
}
