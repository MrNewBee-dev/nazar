using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using BookShop.Models.Repository;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;

namespace Nazar1988.Areas.MyMaster.Controllers
{
    [Area("MyMaster")]
    //[Authorize(Roles = "مدیر")]
    public class ProductsController : Controller
    {
        //public async Task<IActionResult> Edit(int id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    else {

        //        var productid = _context.Products.FindAsync(id);
        //        if (productid == null)
        //        {
        //            return NotFound();
        //        }
        //        else { 
                     
        //        }
        //    }

        //} 
        private readonly NazarDbContext _context;
        private readonly BooksRepository _repository;
        [Obsolete]
        private readonly IWebHostEnvironment _env;
        public ProductsController(NazarDbContext context, BooksRepository repository, IWebHostEnvironment env)
        {
            _context = context;
            _repository = repository;
            _env = env;

        }

        // GET: MyMaster/Products
        public async Task<IActionResult> Index(string Msg,int page=1)
        {
            if (Msg != null)
            {
                ViewBag.Msg = "در ثبت اطلاعات خطایی رخ داده است لطفا مجددا تلاش کنید !!!";
            }
            var pagingModel = PagingList.Create(await _context.Products.ToListAsync(), 8 , page);
            return View(pagingModel);
        }

        // GET: MyMaster/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);

        }
            // GET: MyMaster/Products/Create
            public IActionResult Create()
        {
            
            ProductViewModel ViewModel = new ProductViewModel(_repository. GetAllCategories());
            return View(ViewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit (ProductEditViewModelViews viewModel)
        {
            if (ModelState.IsValid)
            {
                var ProductFound = await _context.Products.FindAsync(viewModel.ProductId);
                if (ProductFound == null)
                {
                    return NotFound();
                }
                ProductFound.IsPublish = viewModel.IsPublish;
                ProductFound.Price= viewModel.Price;
                ProductFound.PriceToziKonande = viewModel.PricetoziKonande;
                ProductFound.ProductDescription = viewModel.ProductDescription;
                ProductFound.ProductName = viewModel.ProductName;
                ProductFound.ProductNumber = viewModel.ProductNumber;
                ProductFound.ProductTotal = viewModel.ProductTotal;
                ProductFound.IsPublish = viewModel.IsPublish;

                _context.Products.Update(ProductFound);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("index");
        }


        // POST: MyMaster/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                List<string> name = new List<string>();
                name.Add("hello.jpg");
                List<Product_Category> categories = new List<Product_Category>();
                var Transaction = _context.Database.BeginTransaction();
                try
                {
                    Product product = new Product()
                    {
                        ProductName = viewModel.ProductName,
                        Price = viewModel.Price,
                        PriceToziKonande = viewModel.PricetoziKonande,
                        ProductDescription = viewModel.ProductDescription,
                        CreateDate = DateTime.Now,
                        ProductNumber = viewModel.ProductNumber,
                        ProductTotal = viewModel.ProductTotal,
                        IsPublish = viewModel.IsPublish,
                        ImagePath = System.Text.Json.JsonSerializer.Serialize(name),
                        DiscountId = new Discount { 
                            DiscountPercent = 0,
                            EndtDate = DateTime.Now,
                            StartDate =DateTime.Now 
                        }
                        

                };
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();



                    if (viewModel.CategoryId != null)
                    {
                        for (int i = 0; i < viewModel.CategoryId.Length; i++)
                        {
                            Product_Category category = new Product_Category()
                            {
                                ProductID = product.ProductID,
                                CategoryID = viewModel.CategoryId[i],
                            };

                            categories.Add(category);
                        }

                        await _context.Product_Categories.AddRangeAsync(categories);
                    }

                    await _context.SaveChangesAsync();
                    Transaction.Commit();
                    return RedirectToAction("Index");
                }
                catch {
                    return RedirectToAction("Index", new { Msg = "Failed" });
                }
                }
            else
            {
                viewModel.Categories = _repository.GetAllCategories();
                return View(viewModel);
            }
        }
        [Route("Upload")]
        public IActionResult FileUploader(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> FileUploader(IEnumerable<IFormFile> files,int id)
        {
            List<string> name = new List<string>();
            var products = _context.Products.Find(id);
            
            if (products == null)
            {
                return NotFound();
            }

            var UploadsRootFoolder = Path.Combine(_env.WebRootPath, "GalleryFiles");
            if (!Directory.Exists(UploadsRootFoolder))
            {
                Directory.CreateDirectory(UploadsRootFoolder);
            }
            foreach (var item in files)
            {
                if (files != null)
                {
                    string FileExtention = Path.GetExtension(item.FileName);

                    if (FileExtention == ".jpg" || FileExtention == ".png")
                    {


                        string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtention);
                        var path = Path.Combine(UploadsRootFoolder, NewFileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            await item.CopyToAsync(stream);
                            name.Add(stream.Name.ToString());

                        }

                    }

                }
            }

            products.ImagePath = System.Text.Json.JsonSerializer.Serialize(name);
         //   var show = System.Text.Json.JsonSerializer.Deserialize<string>(products.ImagePath);

            try
            {
                _context.Update(products);
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException)
            {
             
                    return NotFound();
             
            }

            return new JsonResult("success");
            
        }
    }



}

