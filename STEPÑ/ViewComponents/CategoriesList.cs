using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STEPÑ.Data;
using System.Threading.Tasks;

namespace STEPÑ.ViewComponents
{
    public class CategoriesList : ViewComponent
    {
        private readonly STEPÑContext _context;
        public CategoriesList(STEPÑContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _context.Category.ToListAsync();
            return View(items);
        }
    }
}
