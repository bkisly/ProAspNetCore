using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstProject.Models;

namespace FirstProject.Controllers;

public class HomeController : Controller
{
    public ViewResult Index()
    {
        var viewMdoel = DateTime.Now.Hour >= 12 ? "Good afternoon" : "Good morning";
        return View("MyView", viewMdoel);
    }
}
