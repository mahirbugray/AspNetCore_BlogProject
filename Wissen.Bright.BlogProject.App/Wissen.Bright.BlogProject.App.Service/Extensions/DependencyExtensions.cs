using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.DataAccess.Contexts;
using Wissen.Bright.BlogProject.App.DataAccess.Identity;
using Wissen.Bright.BlogProject.App.DataAccess.Repositories;
using Wissen.Bright.BlogProject.App.DataAccess.UnitOfWorks;
using Wissen.Bright.BlogProject.App.Entity.Repositories;
using Wissen.Bright.BlogProject.App.Entity.Services;
using Wissen.Bright.BlogProject.App.Entity.UnitOfWorks;
using Wissen.Bright.BlogProject.App.Service.Mapping;
using Wissen.Bright.BlogProject.App.Service.Services;

namespace Wissen.Bright.BlogProject.App.Service.Extensions
{
	public static class DependencyExtensions
	{
		public static void AddExtensions(this IServiceCollection services)
		{
			services.AddIdentity<AppUser, AppRole>(
				opt =>
					{
						opt.Password.RequireNonAlphanumeric = false;
						opt.Password.RequiredLength = 3;
						opt.Password.RequireUppercase = false;
						opt.Password.RequireLowercase = false;
						opt.Password.RequireDigit = false;

						//opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyzqw0123456789";

						opt.User.RequireUniqueEmail = true;

						opt.Lockout.MaxFailedAccessAttempts = 3;    //default : 5
						opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //default 5 dk
					}
					).AddEntityFrameworkStores<BlogDbContext>();

			services.ConfigureApplicationCookie(opt =>
				{
					opt.LoginPath = new PathString("/Account/Login");
					opt.LogoutPath = new PathString("/Account/Logout");
					//opt.AccessDeniedPath = new PathString("/Account/AccessDenied");
					opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
					opt.SlidingExpiration = true; //10 dk dolmadan kullanıcı login olursa süre baştan başlar.
					opt.Cookie = new CookieBuilder()
					{
						Name = "Identity.App.Cookie",
						HttpOnly = true
					};
			});

			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUnitOfWorks, UnitOfWork>();
			services.AddScoped(typeof(IAccountService), typeof(AccountService));
			services.AddScoped<ITagService, TagService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped<ICategoryService, CategoryService>();

			services.AddAutoMapper(typeof(MappingProfile));
		}
	}
}
