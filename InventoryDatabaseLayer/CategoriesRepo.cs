using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_DBLibrary;
using InventoryModels.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDatabaseLayer
{
    public class CategoriesRepo : ICategoriesRepo
    {
        private readonly IMapper _mapper;
        private readonly InventoryDbContext _context;
        public CategoriesRepo(InventoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> ListCategoriesAndDetails()
        {
            return await _context.Categories.Include(x => x.CategoryDetail)
                        .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }
    }
}
