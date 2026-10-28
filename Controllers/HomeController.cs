using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactDemo.Models;
using Microsoft.EntityFrameworkCore;
using ContactDemo.Data;

namespace ContactDemo.Controllers;

public class HomeController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;
    public IActionResult Index()
    {
        return View(_context.Contacts);
    }

    public IActionResult Edit(Guid? id)
    {
        var contact = _context.Contacts.Find(id);

        if (contact is null)
        {
            ViewBag.IsNew = true;
        }
        contact ??= new() { FirstName = "", LastName = "" };

        return View(contact);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Guid id, Contact contact)
    {
        if (id != contact.Id)
        {
            return NotFound("Missmatched Ids");
        }

        if (ModelState.IsValid)
        {
            var record = _context.Contacts.Find(id);

            if (record is null)
            {
                record = new Contact { FirstName = "", LastName = "" };
                _context.Contacts.Add(record);
            }

            record.FirstName = contact.FirstName;
            record.LastName = contact.LastName;
            record.Address = contact.Address;
            record.Email = contact.Email;
            record.PhoneNumber = contact.PhoneNumber;
            record.Type = contact.Type;
            record.Birthday = contact.Birthday;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(contact);
    }

    public IActionResult Info(Guid id) => View(_context.Contacts.Find(id));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        var contact = _context.Contacts.Find(id);

        if (contact is null)
        {
            return NotFound();
        }
        _context.Remove(contact);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
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
