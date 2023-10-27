using Microsoft.AspNetCore.Mvc;
using Wissen.Bright.BlogProject.App.Entity.Services;

namespace Wissen.Bright.BlogProject.App.WebMvcUI.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _categoryService.GetAll();
            return View(list);
        }
    }
}
