using Microsoft.AspNetCore.Mvc;

namespace ContactListMvc.Controllers;

public class AddContactFormModel
{
    public required string Name { get; set; }

    public required string Email { get; set; }
}

public class Contact
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}

public class ContactsListViewModel
{
    public List<Contact> Contacts { get; set; } =
    [
        new() { Id = 1, Name = "John Doe", Email = "john1@email.com" },
        new() { Id = 2, Name = "Jane Doe", Email = "jane@email.com" },
        new() { Id = 3, Name = "Bill Doe", Email = "bill@email.com" },
        new() { Id = 4, Name = "Bob Doe", Email = "bob@email.com" },
        new() { Id = 5, Name = "Celine Doe", Email = "celine@email.com" },
    ];

    public AddContactFormModel Form { get; set; } = new()
    {
        Name = "",
        Email = ""
    };

    public List<string> Errors { get; set; } = [];
}

public class ContactsController : Controller
{
    private static readonly ContactsListViewModel ContactsListViewModel = new();

    public IActionResult Index()
    {
        return View(ContactsListViewModel);
    }

    public IActionResult Add(AddContactFormModel model)
    {
        ContactsListViewModel.Errors.Clear();
        if (string.IsNullOrEmpty(model.Name))
        {
            ContactsListViewModel.Errors.Add("Name is required");
        }

        if (string.IsNullOrEmpty(model.Email))
        {
            ContactsListViewModel.Errors.Add("Email is required");
        }

        if (ContactsListViewModel.Errors.Count > 0)
        {
            ContactsListViewModel.Form = model;
            return View("Index", ContactsListViewModel);
        }

        ContactsListViewModel.Contacts.Add(new Contact
        {
            Id = ContactsListViewModel.Contacts.Count,
            Name = model.Name,
            Email = model.Email
        });

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        ContactsListViewModel.Errors.Clear();
        ContactsListViewModel.Contacts.RemoveAll(x => x.Id == id);
        return RedirectToAction("Index");
    }
}
