using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;

    public HomeController(ILogger<HomeController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
    
    public IActionResult Languages()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Send(ContactForm model)
    {
        if (!ModelState.IsValid)
            return View("Contact", model);

        var email = _config["EmailSettings:Email"];
        var password = _config["EmailSettings:Password"];

        try
        {
            var from = new MailAddress(email, "Portfolio");
            var to = new MailAddress(email);

            var mail = new MailMessage(from, to);
            mail.Subject = "New message from portfolio";
            mail.Body =
                $"Name: {model.Name}\n" +
                $"Email: {model.Email}\n\n" +
                model.Message;

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            smtp.Send(mail);

            // автоответ пользователю
            var reply = new MailMessage(
                new MailAddress(email),
                new MailAddress(model.Email)
            );

            reply.Subject = "Thank you for contacting me!";
            reply.Body = "Thank you for your message 🙂";

            smtp.Send(reply);

            return RedirectToAction("Contact");
        }
        catch
        {
            return View("Error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


