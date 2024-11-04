using Microsoft.AspNetCore.Mvc;

namespace ContactListAngularJs.Controllers;

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
}

public class ContactsController : Controller
{
    private static readonly ContactsListViewModel ContactsListViewModel = new();

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("api/contacts")]
    public IActionResult ListContacts()
    {
        return Ok(ContactsListViewModel.Contacts);
    }

    [HttpPost("api/contacts")]
    public IActionResult AddContact([FromBody] AddContactFormModel form)
    {
        if (string.IsNullOrEmpty(form.Name))
        {
            return BadRequest("Name is required");
        }

        if (string.IsNullOrEmpty(form.Email))
        {
            return BadRequest("Email is required");
        }

        ContactsListViewModel.Contacts.Add(new()
        {
            Name = form.Name,
            Email = form.Email
        });

        return Ok();
    }
}
