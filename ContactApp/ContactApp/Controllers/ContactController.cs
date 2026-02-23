using Microsoft.AspNetCore.Mvc;
using ContactApp.Models;

namespace ContactApp.Controllers;

public class ContactController : Controller
{
    private ContactContext context;

    public ContactController(ContactContext ctx)
    {
        context = ctx;
    }

    public IActionResult Index()
    {
        var contacts = context.Contacts
            .OrderBy(c => c.Name)
            .ToList();

        return View(contacts);
    }

    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.Action = "Add";
        return View("Edit", new Contact());
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewBag.Action = "Edit";
        var contact = context.Contacts.Find(id);
        return View(contact);
    }

    [HttpPost]
    public IActionResult Edit(Contact contact)
    {
        if (ModelState.IsValid)
        {
            if (contact.ContactId == 0)
                context.Contacts.Add(contact);
            else
                context.Contacts.Update(contact);

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Action = (contact.ContactId == 0) ? "Add" : "Edit";
        return View(contact);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var contact = context.Contacts.Find(id);
        return View(contact);
    }

    [HttpPost]
    public IActionResult Delete(Contact contact)
    {
        context.Contacts.Remove(contact);
        context.SaveChanges();
        return RedirectToAction("Index");
    }
}