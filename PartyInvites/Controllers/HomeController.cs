using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers;

public class HomeController : Controller
{
    public ViewResult Index()
    {
        return View();
    }

    [HttpGet]
    public ViewResult RsvpForm()
    {
        return View();
    }

    [HttpPost]
    public ViewResult RsvpForm(GuestResponse response)
    {
        if (!ModelState.IsValid)
            return View();

        Repository.AddResponse(response);
        return View("Thanks", response);
    }

    public ViewResult ListResponses()
    {
        return View(Repository.Responses.Where(response => response.WillAttend == true));
    }
}
