using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test3.Data;
using Test3.Models;

namespace Test3.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BookController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {

            var data = _context.Books.ToList();
            return View(data);
        }

        public IActionResult Description(int id)
        {
            var book = _context.Books.FirstOrDefault(s => s.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BookStoreViewmodel model)
        {
            try
            {
                var path = webHostEnvironment.WebRootPath;
                var filepath = "UploadImage/Picture/" + model.Image.FileName;
                var fullpath = Path.Combine(path, filepath);
                Uploadfile(model.Image, fullpath);
                BookStore obj = new BookStore()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Author = model.Author,
                    Image = filepath

                };

                _context.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return View(model);
            }




        }
        private void Uploadfile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }



        [HttpPost]
        public IActionResult Edit(BookStoreViewmodel model)
        {
            var book = _context.Books.FirstOrDefault(a => a.Id == model.Id);

            if (book == null)
            {
                return NotFound();
            }

            try
            {
                var filepath = book.Image;

                if (model.Image != null)
                {
                    var path = webHostEnvironment.WebRootPath;
                    filepath = "UploadImage/Picture/" + model.Image.FileName;
                    var fullpath = Path.Combine(path, filepath);
                    Uploadfile(model.Image, fullpath);
                }

                book.Name = model.Name;
                book.Price = model.Price;
                book.Description = model.Description;
                book.Author = model.Author;
                book.Image = filepath;

                _context.Update(book);
                _context.SaveChanges();
                TempData["Message"] = "Book data edited!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }






        public IActionResult DeleteBook(int id)
        {
            var e = _context.Books.SingleOrDefault(e => e.Id == id);
            _context.Books.Remove(e);
            _context.SaveChanges();
            TempData["error"] = "Record Deleted";
            return RedirectToAction("Index");


        }
    }
}



       
