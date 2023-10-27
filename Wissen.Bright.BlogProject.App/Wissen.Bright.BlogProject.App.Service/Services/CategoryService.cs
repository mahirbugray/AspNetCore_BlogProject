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
    internal class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorks _uow;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IUnitOfWorks uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var list = await _uow.GetRepository<Category>().GetAll(c => c.IsDeleted == false);
            return _mapper.Map<List<CategoryViewModel>>(list);
        }
    }
}
