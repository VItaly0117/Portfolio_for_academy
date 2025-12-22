using About_me.Models;
using About_me.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace About_me.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonDataProvider _provider;
        public Person MyPersonData { get; private set; }
        
        public IndexModel(IPersonDataProvider provider)
        {
            _provider = provider;
        }

        public void OnGet()
        {
            MyPersonData = _provider.GetPersonById(1);
        }
    }
}