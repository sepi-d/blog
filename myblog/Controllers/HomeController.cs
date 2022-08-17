using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using myblog.Models;
using Newtonsoft.Json;

namespace myblog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //todo add try anf catch
        // read Json file 
        var BlogJson = System.IO.File.ReadAllText("App_Data/Blog-Posts.json");
        // convert Json to a generic array 
        var jsonobject = JsonConvert.DeserializeObject(BlogJson);

        // send array to view 

        ViewBag.Posts = jsonobject;

        return View();
    }

  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

