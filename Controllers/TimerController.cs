using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PomoToDoro.Models;

namespace PomoToDoro.Controllers;

public class TimerController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public TimerController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}