using About_me.Models;
using About_me.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace About_me.Pages.Skills
{
    public class CreateModel : PageModel
    {
        private readonly IPersonDataProvider _provider;

        public CreateModel(IPersonDataProvider provider)
        {
            _provider = provider;
        }

        [BindProperty]
        public Skill NewSkill { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(int id = 1)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var person = _provider.GetPersonById(id);
            if (person == null) return NotFound();

            // Ініціалізуємо список, якщо він порожній
            if (person.Skills == null) person.Skills = new List<Skill>();

            // Перевірка на дублікати (опціонально)
            if (!person.Skills.Any(s => s.Name.Equals(NewSkill.Name, StringComparison.OrdinalIgnoreCase)))
            {
                person.Skills.Add(NewSkill);
                _provider.UpdatePerson(person);
            }

            return RedirectToPage("./Index", new { id });
        }
    }
}