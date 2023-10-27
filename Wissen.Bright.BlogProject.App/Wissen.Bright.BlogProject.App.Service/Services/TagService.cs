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
    public class TagService : ITagService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWorks uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<TagViewModel>> GetAll()
        {
            var list = await _uow.GetRepository<Tag>().GetAll();
            return _mapper.Map<List<TagViewModel>>(list);
        }
        public async Task<List<ArticleViewModel>> GetAllArticlesByTag(int Id)
        {
            //Tag tag = await _uow.GetRepository<Tag>().GetById(Id);
            Tag tag = await _uow.GetRepository<Tag>().Get(t => t.Id == Id, null, t => t.Articles); 
            var listArticle = tag.Articles;
            return _mapper.Map<List<ArticleViewModel>>(listArticle);
        }
    }
}
