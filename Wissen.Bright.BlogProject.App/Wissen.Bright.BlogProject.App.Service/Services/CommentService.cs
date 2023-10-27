using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.Entity.Entities;
using Wissen.Bright.BlogProject.App.Entity.Services;
using Wissen.Bright.BlogProject.App.Entity.UnitOfWorks;
using Wissen.Bright.BlogProject.App.Entity.ViewModels;

namespace Wissen.Bright.BlogProject.App.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;

        public CommentService(IMapper mapper, IUnitOfWorks uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task Add(CommentViewModel model)
        {
            Comment comment = new Comment();
            comment = _mapper.Map<Comment>(model);
            _uow.GetRepository<Comment>().Add(comment);
            await _uow.CommitAsync();
        }

        public async Task<List<CommentViewModel>> GetAllArticleId(int id)
        {
            var list = await _uow.GetRepository<Comment>().GetAll(c => c.ArticleId == id);
            return _mapper.Map<List<CommentViewModel>>(list);
        }
    }
}
