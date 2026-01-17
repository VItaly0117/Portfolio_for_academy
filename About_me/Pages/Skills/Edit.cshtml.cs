using About_me.Models;
using About_me.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace About_me.Pages.Skills
{
    public class EditModel : PageModel
    {
        private readonly IPersonDataProvider _provider;

        public EditModel(IPersonDataProvider provider)
        {
            _provider = provider;
        }

        [BindProperty]
        public Skill Skill { get; set; }

        public string OriginalName { get; set; }

        public IActionResult OnGet(string skillName, int id = 1)
        {
            var person = _provider.GetPersonById(id);
            if (person == null) return NotFound();

            var existingSkill = person.Skills.FirstOrDefault(s => s.Name == skillName);
            if (existingSkill == null) return NotFound();

            Skill = existingSkill;
            OriginalName = skillName;

            return Page();
        }

        public IActionResult OnPost(string originalName, int id = 1)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var person = _provider.GetPersonById(id);
            if (person == null) return NotFound();

            // Знаходимо навичку за старою назвою
            var existingSkill = person.Skills.FirstOrDefault(s => s.Name == originalName);
            
            if (existingSkill != null)
            {
                // Оновлюємо поля
                existingSkill.Name = Skill.Name;
                existingSkill.Level = Skill.Level;

                _provider.UpdatePerson(person);
            }

            return RedirectToPage("./Index", new { id });
        }
    }
}