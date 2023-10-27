using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wissen.Bright.BlogProject.App.Entity.Services;
using Wissen.Bright.BlogProject.App.Entity.ViewModels;

namespace Wissen.Bright.BlogProject.App.WebMvcUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly IAccountService _accountService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;
        public ArticleController(IArticleService articleService, ICommentService commentService, IAccountService accountService, ITagService tagService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _commentService = commentService;
            _accountService = accountService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int ? id, string search)
        {
            var list = await _articleService.GetAll();
            var listCategory = await _categoryService.GetAll();

            if (id != null)
            {
                list = list.Where(a => a.CategoryId == id).ToList();
            }
            if (search != null)
            {
                list = list.Where(a => a.Content.ToLower().Contains(search.ToLower())).ToList();
            }
            return View(list);
        }
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Comments = await _commentService.GetAllArticleId(id);
            var model = await _articleService.Get(id);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(int Id, string Message)
        {
            var user = await _accountService.Find(User.Identity.Name);
            CommentViewModel model = new()
            {
                ArticleId = Id,
                Content = Message,
                UserId = user.Id
            };
            await _commentService.Add(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetArticlesByTag(int tagId)
        {
            var list = await _tagService.GetAllArticlesByTag(tagId);
            return View("Index", list);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel model, IFormFile formFile)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile.FileName);
            var stream = new FileStream(path, FileMode.Create);
            formFile.CopyTo(stream);
            model.PictureUrl = "/images" + formFile.FileName + model.Id;    //Yüklenen resim isimlerinde çakışma olmaması için ismin sonuna uniq id bilgisini ekliyoruz.
            var user = await _accountService.Find(User.Identity.Name);
            user.Id = model.UserId;
            await _articleService.Add(model);
            return RedirectToAction("Index");
        }
    }
}



//Lazy Loading -> (Veri yükü baştan az, sonradan devamlı sql'den sorgu çekiyor. entity'ler virtual olmalı.)
//Eager Loading -> (Veri yükü baştan fazla, ama sonradan tekrar tekrar sql'den sorgu çekmiyor. Sorguda ilgili tablolar include ile eklenmeli.)
