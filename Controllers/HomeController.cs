using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactDemo.Models;
using Microsoft.EntityFrameworkCore;
using ContactDemo.Data;

namespace ContactDemo.Controllers;

public class HomeController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;
    public IActionResult Index() => View(from c in _context.Contacts orderby c.FirstName, c.LastName select c);

    public IActionResult Edit(Guid? id)
    {
        var contact = id is Guid contactId
            ? _context.Contacts
                .SingleOrDefault(x => x.Id == contactId)
            : null;

        if (contact is null)
        {
            ViewBag.IsNew = true;
        }
        contact ??= new() { Address = new Address() };
        contact.Address ??= new Address();

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

        if (contact.Address is { } address &&
            string.IsNullOrWhiteSpace(address.Line) &&
            string.IsNullOrWhiteSpace(address.Line2) &&
            string.IsNullOrWhiteSpace(address.Town) &&
            string.IsNullOrWhiteSpace(address.State) &&
            string.IsNullOrWhiteSpace(address.Zip))
        {
            contact.Address = null;
            ModelState.Remove("Address.Line");
            ModelState.Remove("Address.Line2");
            ModelState.Remove("Address.Town");
            ModelState.Remove("Address.State");
            ModelState.Remove("Address.Zip");
        }

        if (ModelState.IsValid)
        {
            var record = _context.Contacts.SingleOrDefault(x => x.Id == id);

            if (record is null)
            {
                record = new Contact();
                _context.Add(record);
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

        ViewBag.IsNew = id == Guid.Empty;
        contact.Address ??= new Address();
        return View(contact);
    }

    public IActionResult Info(Guid id) => View(_context.Contacts.SingleOrDefault(x => x.Id == id));

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

    public IActionResult Locations(ContactType? type) => View(from c in _context.Contacts
                                                              orderby c.FirstName, c.LastName
                                                              where type == null || c.Type == type
                                                              select c);
}
