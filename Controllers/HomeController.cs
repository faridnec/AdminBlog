using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;
using Microsoft.AspNetCore.Localization;

namespace AdminBlog.Controllers;

//[Filter.UserFilter]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;//veri tabani context sinifi tanitma

    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Login(string Email, string Password)
    {
        var author = _context.Author.FirstOrDefault(w => w.Email == Email && w.Password == Password);
        if (author == null)
        {
            return RedirectToAction(nameof(Index));
        }
        HttpContext.Session.SetInt32("id",author.Id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AddCategory(Category category){ //Category Nesnesi
        //Update icin add ile ayni form ile kullaniyorum
        if(category.Id == 0){//eger daha oncesinde veritabaninda bir kayit yoksa add
            await _context.AddAsync(category);
        }else{//var ise update
            _context.Update(category);
        }
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Category));
    }

    public async Task<IActionResult> AddAuthor(Author author){ //Category Nesnesi
        //Update icin add ile ayni form ile kullaniyorum
        if(author.Id == 0){//eger daha oncesinde veritabaninda bir kayit yoksa add
            await _context.AddAsync(author);
        }else{//var ise update
            _context.Update(author);
        }
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Author));
    }

    public async Task<IActionResult> CategoryDetails(int Id)
    {
        var category = await _context.Category.FindAsync(Id);
        return Json(category);//Category modeli JSON olarak return ediyoruz
    }

    public async Task<IActionResult> AuthorDetails(int Id)
    {
        var author = await _context.Author.FindAsync(Id);
        return Json(author);//Author modeli JSON olarak return ediyoruz
    }

    public IActionResult Category()//Add category to list and presented in a table
    {
        List<Category> list = _context.Category.ToList();
        return View(list);
    }

    public IActionResult Author()//Add category to list and presented in a table
    {
        List<Author> list = _context.Author.ToList();
        return View(list);
    }

    public async Task<IActionResult> DeleteCategory(int? Id)
    {
        Category category = await _context.Category.FindAsync(Id);
        _context.Remove(category);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Category));
    }

        public async Task<IActionResult> DeleteAuthor(int? Id)
    {
        var author = await _context.Author.FindAsync(Id);
        _context.Remove(author);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Author));
    }

    public IActionResult LogOut(){
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ChangeLanguage(string culture)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return Redirect(Request.Headers["Referer"].ToString());
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
}
