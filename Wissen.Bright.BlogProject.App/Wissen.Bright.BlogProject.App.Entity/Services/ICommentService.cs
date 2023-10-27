using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.Entity.ViewModels;

namespace Wissen.Bright.BlogProject.App.Entity.Services
{
    public interface ICommentService
    {
        Task<List<CommentViewModel>> GetAllArticleId(int id);
        Task Add(CommentViewModel model);
    }
}
