using Microsoft.AspNetCore.Mvc;
using Wissen.Bright.BlogProject.App.Entity.Services;

namespace Wissen.Bright.BlogProject.App.WebMvcUI.ViewComponents
{
    public class TagsViewComponent : ViewComponent
    {
        private readonly ITagService _service;

        public TagsViewComponent(ITagService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _service.GetAll();
            return View(list);
        }
    }
}
