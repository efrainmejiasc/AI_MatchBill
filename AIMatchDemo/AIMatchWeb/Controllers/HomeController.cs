using AIMatchWeb.Business.Interfaces;
using AIMatchWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AIMatchWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPdfToJsonBusiness _pdfToJsonBusiness;

        public HomeController(IPdfToJsonBusiness pdfToJsonBusiness)
        {
            this._pdfToJsonBusiness = pdfToJsonBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var jsonBill1 = string.Empty;
            var jsonBill2 = string.Empty;

            var response = new GenericResponseModelDto()
            {
                Estatus = false,
                HttpCode = 400
            };

            if (files == null || files.Count == 0)
                return Json(response);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);

                    // Guardar archivo en el servidor
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    if (string.IsNullOrEmpty(jsonBill1))
                        jsonBill1 = this._pdfToJsonBusiness.ConvertPDFToJson(filePath, file.FileName);
                    else
                        jsonBill2 = this._pdfToJsonBusiness.ConvertPDFToJson(filePath, file.FileName);

                }
            }
            response.Estatus = true;
            response.HttpCode = 200;
            return Json(response);
        }
    }
}
