using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using myblog.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace myblog.Controllers;

public class HomeController : Controller
{
    string jsonFilePath = "App_Data/Blog-Posts.json";

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //todo add try anf catch
        // read Json file 
        var BlogJson = System.IO.File.ReadAllText(jsonFilePath);
        // convert Json to a generic array 
        var jsonobject = JsonConvert.DeserializeObject(BlogJson);

        // send array to view 
        ViewBag.Posts = jsonobject;

        return View();
    }

    // POST: 
    [HttpPost]
    public IActionResult Comment()
    {
        var postId = Request.Form["PostId"];
        var name = Request.Form["Name"];
        var email = Request.Form["Email"];
        var message = Request.Form["Message"];
        var datestr = DateTime.Now;

        dynamic commnet = new comment();
        commnet.name = name;
        commnet.emailAddress = email;
        commnet.message = message;
        commnet.date = datestr;

        var BlogJson = System.IO.File.ReadAllText(jsonFilePath);

        // 1. convert BlogJson to array
        dynamic blogObj = JsonConvert.DeserializeObject(BlogJson);
        dynamic commentJson = JsonConvert.SerializeObject(commnet);
        dynamic commentObj = JsonConvert.DeserializeObject(commentJson);

        //find postId in array key
        foreach (dynamic postt in blogObj.blogPosts)
        {
            //add commnet to array for given post
            if (postt.id == postId.ToString())
            {
                // check if there is no comments yet, then add key
                if (postt.comments == null)
                {
                    postt.comments = JsonConvert.DeserializeObject("[]");
                }
                postt.comments.Add(commentObj);

            }
        }
        
        // convert array to json, save json
        var newBlogJson = JsonConvert.SerializeObject(blogObj, Formatting.Indented);
        System.IO.File.WriteAllText(jsonFilePath, newBlogJson);

        // now go to index
       return RedirectToAction("Index", "Home");
    }







    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

