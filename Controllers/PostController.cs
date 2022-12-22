using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminBlog.Controllers;
//[Filter.UserFilter]
public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    private readonly BlogContext _context;//veri tabani context sinifi tanitma

    public PostController(ILogger<PostController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var list = _context.Blog.Take(4).Where(b => b.IsPublish).OrderByDescending(x => x.CreateTime).ToList();//display 4 blogs (able to be modified)
        foreach (var blog in list){
            blog.Author = _context.Author.Find(blog.AuthorId);//id si verilen yazari blog'un yazarina ekliyor
        }
        return View(list);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Post(int Id)
    {
        var blog = _context.Blog.Find(Id);
        blog.Author = _context.Author.Find(blog.AuthorId); //yazar atama. null reference hatasi giderilmesi
        blog.ImagePath = "/img/"+blog.ImagePath;
        return View(blog);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
