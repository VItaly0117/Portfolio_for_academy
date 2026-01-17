using About_me.Models;
using About_me.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace About_me.Pages.Skills
{
    public class IndexModel : PageModel
    {
        private readonly IPersonDataProvider _provider;

        public IndexModel(IPersonDataProvider provider)
        {
            _provider = provider;
        }

        public Person Person { get; set; }

        // За замовчуванням беремо ID = 1, як у вашому прикладі
        public void OnGet(int id = 1)
        {
            Person = _provider.GetPersonById(id);
        }

        public IActionResult OnPostDelete(string skillName, int id = 1)
        {
            var person = _provider.GetPersonById(id);
            if (person != null)
            {
                // Знаходимо та видаляємо навичку за назвою
                var skillToRemove = person.Skills.FirstOrDefault(s => s.Name == skillName);
                if (skillToRemove != null)
                {
                    person.Skills.Remove(skillToRemove);
                    _provider.UpdatePerson(person);
                }
            }
            TempData["Success"] = "Skill created successfully!";
            return RedirectToPage(new { id });
        }
    }
}