using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GummyBearKingdom.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummyBearKingdom.Controllers
{
    public class ProductController : Controller
    {
        public ProductController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        BearDbContext db = new BearDbContext();

        public IHostingEnvironment HostingEnvironment { get; }

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public IActionResult Details(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var imgFiles = HttpContext.Request.Form.Files;
            foreach (var pic in imgFiles)
            {
                if (pic != null && pic.Length > 0)
                {
                    var file = pic;
                    var uploads = Path.Combine(HostingEnvironment.WebRootPath, "img");
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        Console.WriteLine(fileName);

                        var newFileName = Guid.NewGuid().ToString() + file.FileName;
                        using (var fileStream = new FileStream(Path.Combine(uploads, newFileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            product.Image = newFileName;
                        }
                    }
                }
            }

            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(x => x.ProductId == id);
            db.Products.Remove(thisProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
