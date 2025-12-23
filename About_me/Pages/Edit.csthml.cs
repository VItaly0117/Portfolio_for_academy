using About_me.Models;
using About_me.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace About_me.Pages
{
    public class EditModel : PageModel
    {
        private readonly IPersonDataProvider _provider;

        [BindProperty] // Дозволяє формі автоматично заповнювати цю властивість при відправці (POST)
        public Person Person { get; set; }

        public EditModel(IPersonDataProvider provider)
        {
            _provider = provider;
        }

        // Завантаження даних при відкритті сторінки
        public void OnGet(int id)
        {
            Person = _provider.GetPersonById(id);
        }

        // Обробка кнопки "Зберегти"
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _provider.UpdatePerson(Person);

            return RedirectToPage("Index"); // Повертаємось на головну після збереження
        }
    }
}